using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaStreaming 
{
    private string nombre;
    private string urlBase;
    public List<Contenido> contenidoDisponible;

    public PlataformaStreaming(string nombre, string urlBase)
    {
        this.nombre = nombre;
        this.urlBase = urlBase;
        this.contenidoDisponible = new List<Contenido>();
    }   

    public void agregarContenido(Contenido contenido)
    {
        contenidoDisponible.Add(contenido);
    }

    public Contenido buscarContenido(string titulo)
    {
        foreach (var contenido in contenidoDisponible)
        {
            if (contenido.getTitulo() == titulo)
            {
                return contenido;
            }
        }
        throw new Exception($"El contenido '{titulo}' no existe en esta plataforma.");
    }

    public void eliminarContenido(string titulo)
    {
        Contenido contenidoAEliminar = buscarContenido(titulo);
        if (contenidoAEliminar != null)
        {
            contenidoDisponible.Remove(contenidoAEliminar);
        }
    }
}
