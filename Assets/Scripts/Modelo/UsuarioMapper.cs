using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsuarioMapper
{
    private const string UsernameAdmin = "admin";

    private readonly IRol rolNormal = new RolNormal();
    private readonly Dictionary<string, IRol> rolesDefinidos;

    public UsuarioMapper()
    {
        rolesDefinidos = new Dictionary<string, IRol>(StringComparer.OrdinalIgnoreCase)
        {
            { UsernameAdmin, new RolAdmin() }
        };
    }

    public Usuario ADominio(UsuarioData d)
    {
        if (d == null) throw new ArgumentNullException(nameof(d));

        string username = d.login?.username ?? "";
        rolesDefinidos.TryGetValue(username, out IRol rol);

        return new Usuario
        {
            Nombre = d.name?.first ?? "",
            Apellido = d.name?.last ?? "",
            Edad = (d.dob?.age ?? 0).ToString(),
            Telefono = d.phone ?? "",
            Email = d.email ?? "",
            Username = username,
            Password = d.login?.password ?? "",
            FotoUrl = d.picture?.large ?? "",
            Direccion = ArmarDireccion(d.location),
            Rol = rol ?? rolNormal
        };
    }

    public UsuarioData ADto(Usuario u)
    {
        if (u == null) throw new ArgumentNullException(nameof(u));

        int.TryParse(u.Edad, out int edad); // Si no es numérico queda en 0, no rompe el guardado

        return new UsuarioData
        {
            name = new NameData { first = u.Nombre, last = u.Apellido },
            dob = new DobData { age = edad },
            phone = u.Telefono,
            email = u.Email,
            login = new LoginData { username = u.Username, password = u.Password },
            picture = new PictureData { large = u.FotoUrl },
            location = new LocationData
            {
                street = new StreetData { name = u.Direccion, number = 0 },
                city = "",
                state = "",
                country = ""
            }
        };
    }

    private string ArmarDireccion(LocationData loc)
    {
        if (loc == null) return "";

        string calle = loc.street == null
            ? ""
            : (loc.street.number > 0 ? $"{loc.street.name} {loc.street.number}" : loc.street.name);

        List<string> partes = new List<string>();
        foreach (string p in new[] { calle, loc.city, loc.country })
        {
            if (!string.IsNullOrWhiteSpace(p)) partes.Add(p.Trim());
        }
        return string.Join(", ", partes);
    }
}