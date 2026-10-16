using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RandomUserApi : IApiUsuarios
{
    private const string Url = "https://randomuser.me/api/?results=10&nat=us,es,gb";
    private const int TimeoutSegundos = 15;

    private readonly MonoBehaviour despachadorCorrutinas;
    private readonly UsuarioMapper mapper;

    public RandomUserApi(MonoBehaviour despachador, UsuarioMapper mapper)
    {
        despachadorCorrutinas = despachador ?? throw new ArgumentNullException(nameof(despachador));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public void DescargarNuevosUsuariosAPI(Action<List<Usuario>> onExito, Action<string> onError)
    {
        despachadorCorrutinas.StartCoroutine(RutinaDescarga(onExito, onError));
    }

    private IEnumerator RutinaDescarga(Action<List<Usuario>> onExito, Action<string> onError)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Url))
        {
            www.timeout = TimeoutSegundos;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke("No se pudieron descargar los usuarios: " + www.error);
                yield break;
            }

            if (!TryParsear(www.downloadHandler.text, out RespuestaAPI datos))
            {
                onError?.Invoke("La respuesta del servidor no tiene un formato válido.");
                yield break;
            }

            // Forzamos el admin en el primer resultado
            datos.results[0].login = new LoginData { username = "admin", password = "admin" };

            List<Usuario> lista = new List<Usuario>();
            foreach (UsuarioData d in datos.results)
            {
                if (d != null) lista.Add(mapper.ADominio(d));
            }

            onExito?.Invoke(lista);
        }
    }

    private static bool TryParsear(string json, out RespuestaAPI datos)
    {
        datos = null;
        try
        {
            datos = JsonUtility.FromJson<RespuestaAPI>(json);
        }
        catch (ArgumentException)
        {
            return false;
        }
        return datos?.results != null && datos.results.Length > 0 && datos.results[0] != null;
    }
}