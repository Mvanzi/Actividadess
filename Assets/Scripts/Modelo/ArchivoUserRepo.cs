using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArchivoUserRepo : IUserRepo
{
    private readonly string rutaArchivo;
    private readonly UsuarioMapper mapper;

    public ArchivoUserRepo(UsuarioMapper mapper)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        rutaArchivo = Path.Combine(Application.streamingAssetsPath, "usuarios.json");
    }

    public Usuario Autenticar(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
            throw new UsuarioNoExisteException("El usuario no existe.");

        RespuestaAPI baseDeDatos = CargarJson();
        UsuarioData data = Array.Find(baseDeDatos.results, u => u?.login != null && u.login.username == username);

        if (data == null)
            throw new UsuarioNoExisteException("El usuario no existe.");

        if (data.login.password != password)
            throw new ContraseniaIncorrectaException("La contraseña es incorrecta.");

        return mapper.ADominio(data);
    }

    public List<Usuario> ObtenerTodos()
    {
        RespuestaAPI baseDeDatos = CargarJson();
        List<Usuario> lista = new List<Usuario>();
        foreach (UsuarioData d in baseDeDatos.results)
        {
            if (d != null) lista.Add(mapper.ADominio(d));
        }
        return lista;
    }

    public void ReemplazarUsuarios(List<Usuario> nuevos)
    {
        if (nuevos == null || nuevos.Count == 0)
            throw new ArgumentException("La lista de usuarios nuevos no puede estar vacía.", nameof(nuevos));

        RespuestaAPI nuevaDB = new RespuestaAPI { results = new UsuarioData[nuevos.Count] };
        for (int i = 0; i < nuevos.Count; i++)
            nuevaDB.results[i] = mapper.ADto(nuevos[i]);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(rutaArchivo));
            File.WriteAllText(rutaArchivo, JsonUtility.ToJson(nuevaDB, true));
        }
        catch (IOException ex)
        {
            throw new ErrorPersistenciaException("No se pudo guardar el archivo de usuarios.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new ErrorPersistenciaException("No se pudo guardar el archivo de usuarios.", ex);
        }
    }

    private RespuestaAPI CargarJson()
    {
        try
        {
            if (!File.Exists(rutaArchivo)) return BaseVacia();

            RespuestaAPI db = JsonUtility.FromJson<RespuestaAPI>(File.ReadAllText(rutaArchivo));
            return db?.results != null ? db : BaseVacia();
        }
        catch (IOException ex) { return BaseVaciaPorError(ex); }
        catch (UnauthorizedAccessException ex) { return BaseVaciaPorError(ex); }
        catch (ArgumentException ex) { return BaseVaciaPorError(ex); }
    }

    private static RespuestaAPI BaseVaciaPorError(Exception ex)
    {
        Debug.LogError("No se pudo leer usuarios.json: " + ex.Message);
        return BaseVacia();
    }

    private static RespuestaAPI BaseVacia()
    {
        return new RespuestaAPI { results = new UsuarioData[0] };
    }
}