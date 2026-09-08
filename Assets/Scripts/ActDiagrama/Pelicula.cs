using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelicula : Contenido, IReproducible
{
    private string director;
    private int anioEstreno;

    public Pelicula(string titulo,int duracion,string genero, DateTime fechaLanzamiento, string director, int anioEstreno)
        : base(titulo, duracion, genero, fechaLanzamiento)
    {
        this.director = director;
        this.anioEstreno = anioEstreno;
    }

    public string obtenerInformacionPelicula()
    {
        return $"{obtenerInformacion()}, Director: {director}, Año de Estreno: {anioEstreno}";
    }

    public void reproducir()
    {
        Debug.Log($"Reproduciendo la película: {getTitulo()}");
    }

    public void pausar()
    {
        Debug.Log($"Pausando la película: {getTitulo()}");
    }

    public void detener()
    {
        Debug.Log($"Deteniendo la película: {getTitulo()}");
    }
}
