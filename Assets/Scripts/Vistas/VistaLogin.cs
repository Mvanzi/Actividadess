using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VistaLogin : MonoBehaviour, ILoginView
{
    public TMP_InputField inputUsuario;
    public TMP_InputField inputPassword;
    public TextMeshProUGUI textoErrorUsuario;
    public TextMeshProUGUI textoErrorPassword;

    public string Username => inputUsuario.text;
    public string Password => inputPassword.text;
    public Action AlHacerLogin { get; set; }

    public void MostrarErrorUsuario(string mensaje) { MostrarTexto(textoErrorUsuario, mensaje); }
    public void MostrarErrorPassword(string mensaje) { MostrarTexto(textoErrorPassword, mensaje); }

    // Reutiliza el campo de error de contraseña para no exigir un nuevo objeto en el Inspector.
    public void MostrarErrorGeneral(string mensaje) { MostrarTexto(textoErrorPassword, mensaje); }

    public void LimpiarCampos()
    {
        inputUsuario.text = "";
        inputPassword.text = "";
        OcultarErrores();
    }

    // CONECTAR ESTE MÉTODO AL BOTÓN DE LOGIN EN EL INSPECTOR (Desde el propio panel, no desde el Canvas)
    public void BotonLogin_Click()
    {
        OcultarErrores();
        AlHacerLogin?.Invoke();
    }

    private void OcultarErrores()
    {
        textoErrorUsuario.gameObject.SetActive(false);
        textoErrorPassword.gameObject.SetActive(false);
    }

    private static void MostrarTexto(TextMeshProUGUI campo, string mensaje)
    {
        campo.text = mensaje;
        campo.gameObject.SetActive(true);
    }
}
