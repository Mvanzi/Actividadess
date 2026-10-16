using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VistaPerfil : VistaConImagen, IPerfilView
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoApellido;
    public TextMeshProUGUI textoEdad;
    public TextMeshProUGUI textoTelefono;
    public TextMeshProUGUI textoDireccion;
    public TextMeshProUGUI textoEmail;

    public Action AlHacerLogout { get; set; }

    public void MostrarDatosPerfil(DatosUsuarioVista datos)
    {
        textoNombre.text = "Nombre: " + datos.Nombre;
        textoApellido.text = "Apellido: " + datos.Apellido;
        textoEdad.text = "Edad: " + datos.Edad;
        textoTelefono.text = "Teléfono: " + datos.Telefono;
        textoEmail.text = "Email: " + datos.Email;
        textoDireccion.text = "Dirección:\n" + datos.Direccion;
        MostrarImagen(datos.FotoUrl);
    }

    // CONECTAR AL BOTÓN DE SALIR
    public void BotonLogout_Click() { AlHacerLogout?.Invoke(); }
}