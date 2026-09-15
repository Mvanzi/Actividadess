using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancion : Contenido, IReproducible
{
    private string artista;
    private string album;

    public Cancion(string titulo, int duracionMinutos, string genero, DateTime fechaLanzamiento, string artista, string album)
        : base(titulo, duracionMinutos, genero, fechaLanzamiento)
    {
        this.artista = artista;
        this.album = album;
    }   

    public string obtenerInformacionCancion()
    {
        return $"{obtenerInformacion()}, Artista: {artista}, Álbum: {album}";
    }

    public override void reproducir()
    {
        Debug.Log($"Reproduciendo la canción: {getTitulo()}");
    }

    public override void pausar()
    {
        Debug.Log($"Pausando la canción: {getTitulo()}");
    }   

    public override void detener()
    {
        Debug.Log($"Deteniendo la canción: {getTitulo()}");
    }   

}
