using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serie : Contenido, IReproducible
{
    private int numeroTemporadas;
    private Dictionary<int, int> episodiosPorTemporada;

    public Serie(string titulo, int duracion, string genero, DateTime fechaLanzamiento, int numeroTemporadas, Dictionary<int, int> episodiosPorTemporada)
        : base(titulo, duracion, genero, fechaLanzamiento)
    {
        this.numeroTemporadas = numeroTemporadas;
        this.episodiosPorTemporada = episodiosPorTemporada;
    }

    public int obtenerTotalEpisodios()
    {
        int totalEpisodios = 0;
        foreach (var episodios in episodiosPorTemporada.Values)
        {
            totalEpisodios += episodios;
        }
        return totalEpisodios;
    }

    public string obtenerInformacionSerie()
    {
        return $"{obtenerInformacion()}, Número de Temporadas: {numeroTemporadas}, Total de Episodios: {obtenerTotalEpisodios()}";
    }

    public void reproducir()
    {
        Debug.Log($"Reproduciendo la serie: {getTitulo()}");
    }

    public void pausar()
    {
        Debug.Log($"Pausando la serie: {getTitulo()}");
    }   

    public void detener()
    {
        Debug.Log($"Deteniendo la serie: {getTitulo()}");
    }
}

