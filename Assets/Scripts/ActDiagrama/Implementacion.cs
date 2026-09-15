using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Implementacion
{
    public static void Main(string[] args)
    {

        Contenido cancion = new Cancion("Still D.R.E.", 4, "Hip Hop", new System.DateTime(1999, 11, 2), "Dr. Dre ft. Snoop Dogg", "2001");
        Contenido podcast = new Podcast("The Wild Project", 180, "Entrevistas", new System.DateTime(2024, 2, 20), "Jordi Wild", "Actualidad y debate");
        Contenido pelicula = new Pelicula("Tren Bala", 126, "Acción", new System.DateTime(2022, 8, 5), "David Leitch", 2022);
        Contenido serie = new Serie("The Office", 22, "Comedia", new System.DateTime(2005, 3, 24), 9);

        PlataformaStreaming menflis = new PlataformaStreaming("Menflis", "www.menflis.com");
        PlataformaStreaming musicYA = new PlataformaStreaming("MusicYA", "www.musicya.com");

        menflis.agregarContenido(pelicula);
        menflis.agregarContenido(serie);

        musicYA.agregarContenido(cancion);
        musicYA.agregarContenido(podcast);


        Usuario user1 = new Usuario("123", "juan perez", "juancitoperez777@hotmail.com");
        Usuario user2 = new Usuario("456", "juan martin", "juanma666@hotmail.com");

        foreach (var contenido in menflis.contenidoDisponible)
        {
            contenido.obtenerInformacion();
        }

        foreach (var contenido in musicYA.contenidoDisponible)
        {
            contenido.obtenerInformacion();
        }

        user1.registrarSuscripcion(menflis);
        user1.verContenido(pelicula);

        user1.registrarSuscripcion(musicYA);
        user1.verContenido(cancion);

        try { 

            Contenido peliculaBuscada = menflis.buscarContenido("Tren Bala");
            Contenido peliculaQNoEsta = menflis.buscarContenido("Pelicula que no existe");
        }
        catch (Exception ex)
{
            Console.WriteLine("Ocurrió un error: " + ex.Message);
        }

        int episodios = ((Serie)serie).obtenerTotalEpisodios();

    }
}

