using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Podcast : Contenido, IReproducible
{
    private string presentador;
    private string tema;

    public Podcast(string titulo, int duracionMinutos, string genero, DateTime fechaLanzamiento, string presentador, string tema)
        : base(titulo, duracionMinutos, genero, fechaLanzamiento)
    {
        this.presentador = presentador;
        this.tema = tema;
    }

    public string obtenerInformacionPodcast()
    {
        return $"{obtenerInformacion()}, Presentador: {presentador}, Tema: {tema}";
    }
    public override void reproducir()
    {
        Debug.Log($"Reproduciendo el podcast: {getTitulo()}");
    }

    public override void pausar()
    {
        Debug.Log($"Pausando el podcast: {getTitulo()}");
    }

    public override void detener()
    {
        Debug.Log($"Deteniendo el podcast: {getTitulo()}");
    }
}
