using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Contenido : IReproducible
{
    protected string titulo;
    protected int duracionMinutos;
    protected string genero;
    protected DateTime fechaLanzamiento;

    public Contenido(string titulo, int duracionMinutos, string genero, DateTime fechaLanzamiento)
    {
        this.titulo = titulo;
        this.duracionMinutos = duracionMinutos;
        this.genero = genero;
        this.fechaLanzamiento = fechaLanzamiento;
    }   

    public string getTitulo()
    {
        return titulo;
    }   

    public int getDuracion()
    {
        return duracionMinutos;
    }

    public string obtenerInformacion()
    {
        return $"Título: {titulo}, Duración: {duracionMinutos} minutos, Género: {genero}, Fecha de Lanzamiento: {fechaLanzamiento.ToShortDateString()}";
    }

    public abstract void reproducir();
    public abstract void pausar();

    public abstract void detener();
}
