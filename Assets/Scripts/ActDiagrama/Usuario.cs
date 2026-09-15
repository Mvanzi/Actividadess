using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usuario 
{
    private string idUsuario;
    private string nombre;
    private string email;
    private List<PlataformaStreaming> suscripciones;

    public Usuario(string idUsuario, string nombre, string email)
    {
        this.idUsuario = idUsuario;
        this.nombre = nombre;
        this.email = email;
        this.suscripciones = new List<PlataformaStreaming>();
    }

    public void registrarSuscripcion(PlataformaStreaming plataforma)
    {
        suscripciones.Add(plataforma);
    }

    public void cancelarSuscripcion(PlataformaStreaming plataforma)
    {
        suscripciones.Remove(plataforma);
    }

    public void verContenido(Contenido contenido)
    {
        contenido.reproducir();
    }
}   