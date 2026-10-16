using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosUsuarioVista
{
    public string Nombre { get; }
    public string Apellido { get; }
    public string Edad { get; }
    public string Telefono { get; }
    public string Direccion { get; }
    public string Email { get; }
    public string FotoUrl { get; }

    public DatosUsuarioVista(string nombre, string apellido, string edad, string telefono,
                             string direccion, string email, string fotoUrl)
    {
        Nombre = nombre;
        Apellido = apellido;
        Edad = edad;
        Telefono = telefono;
        Direccion = direccion;
        Email = email;
        FotoUrl = fotoUrl;
    }
}