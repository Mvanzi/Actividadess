using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VistaListaUsuarios : VistaConImagen, IListaUsuariosView
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoApellido;
    public TextMeshProUGUI textoEdad;
    public TextMeshProUGUI textoTelefono;
    public TextMeshProUGUI textoDireccion;
    public TextMeshProUGUI textoEmail;
    public Button botonAnterior;
    public Button botonSiguiente;

    public Action AlHacerSiguiente { get; set; }
    public Action AlHacerAnterior { get; set; }
    public Action AlHacerLogout { get; set; }

    public void MostrarDatosUsuario(DatosUsuarioVista datos)
    {
        textoNombre.text = "Nombre: " + datos.Nombre;
        textoApellido.text = "Apellido: " + datos.Apellido;
        textoEdad.text = "Edad: " + datos.Edad;
        textoTelefono.text = "Teléfono: " + datos.Telefono;
        textoEmail.text = "Email: " + datos.Email;
        textoDireccion.text = "Dirección:\n" + datos.Direccion;
        MostrarImagen(datos.FotoUrl);
    }

    public void MostrarSinUsuarios()
    {
        textoNombre.text = "No hay usuarios para mostrar.";
        textoApellido.text = "";
        textoEdad.text = "";
        textoTelefono.text = "";
        textoEmail.text = "";
        textoDireccion.text = "";
        LimpiarImagen();
        DeshabilitarBotonAnterior();
        DeshabilitarBotonSiguiente();
    }

    public void DeshabilitarBotonAnterior() { botonAnterior.interactable = false; }
    public void DeshabilitarBotonSiguiente() { botonSiguiente.interactable = false; }
    public void HabilitarBotonAnterior() { botonAnterior.interactable = true; }
    public void HabilitarBotonSiguiente() { botonSiguiente.interactable = true; }

    // CONECTAR A LOS BOTONES DE PANEL ADMIN
    public void BotonAnterior_Click() { AlHacerAnterior?.Invoke(); }
    public void BotonSiguiente_Click() { AlHacerSiguiente?.Invoke(); }
    public void BotonLogout_Click() { AlHacerLogout?.Invoke(); }
}
