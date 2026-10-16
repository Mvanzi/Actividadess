using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VistaMenuAdmin : MonoBehaviour, IMenuAdminView
{
    // OPCIONAL: asignar un texto en el Inspector para ver los errores en pantalla.
    public TextMeshProUGUI textoError;

    public Action AlSolicitarVerUsuarios { get; set; }
    public Action AlSolicitarGenerarNuevos { get; set; }

    public void MostrarError(string mensaje)
    {
        Debug.LogWarning(mensaje);
        if (textoError == null) return;
        textoError.text = mensaje;
        textoError.gameObject.SetActive(true);
    }

    // CONECTAR A LOS BOTONES RESPECTIVOS DE VERO CARGAR
    public void BotonVerUsuarios_Click() { OcultarError(); AlSolicitarVerUsuarios?.Invoke(); }
    public void BotonGenerarNuevos_Click() { OcultarError(); AlSolicitarGenerarNuevos?.Invoke(); }

    private void OcultarError()
    {
        if (textoError != null) textoError.gameObject.SetActive(false);
    }
}