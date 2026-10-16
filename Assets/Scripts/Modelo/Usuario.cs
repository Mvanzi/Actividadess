using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usuario
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Edad { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FotoUrl { get; set; }
    public IRol Rol { get; set; }
}
