using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.IO;

public class ActividadDicc : MonoBehaviour
{
    private void Start()
    {
        int n = 100;
        int a = 10;
        int b = 10;
        int minimo = 1;
        int maximo = 100;

        List<int> lista = new List<int>(n);
        int[,] matrix = new int[a, b];

        System.Random generador = new System.Random();
        int numAleatorio = generador.Next(minimo, maximo);

        for (int i = 0; i < n; i++)
        {
            lista.Add(generador.Next(minimo, maximo));
        }

        int indice = 0;

        for (int i = 0; i < a; i++)
        {
            for (int j = 0; j < b; j++)
            {
                matrix[i, j] = lista[indice];
                indice++;
            }
        }

        for (int i = 0; i < a; i++)
        {
            string fila = "";
            for (int j = 0; j < b; j++)
            {
                fila += matrix[i, j] + "\t";
            }
            Debug.Log(fila);
        }

        List<int> lista2 = new List<int>(a);

        for (int i = 0; i < a; i++)
        {
            lista2.Add(generador.Next(minimo, maximo));
        }

        // Cambiamos 'a' por 'n' como pide el enunciado
        int[] array = new int[n];

        int intentos = 0;
        int validosGuardados = 0;

        // Mientras no hayamos llenado los 100 lugares (n) del arreglo...
        while (validosGuardados < n)
        {
            // 1. Generamos un número al azar y sumamos un intento
            int numeroAlAzar = generador.Next(minimo, maximo);
            intentos++;

            // 2. Nos fijamos si el número NO está en tu lista de 10 (lista2)
            if (!lista2.Contains(numeroAlAzar))
            {
                // 3. Como no está en la lista negra, lo guardamos y avanzamos un lugar
                array[validosGuardados] = numeroAlAzar;
                validosGuardados++;
            }
        }

        Debug.Log("Cantidad total de intentos: " + intentos);
        Debug.Log("Lista negra: " + string.Join(", ", lista2));
        Debug.Log("Arreglo final: " + string.Join(", ", array));

        List<int> sinpa = new List<int>();

        int indice2 = n - 1;

        while (indice2 > -1)
        {
            int i = 0;
            if (array[indice2] % 2 != 0)
            {
                sinpa.Add(array[indice2]);
            }
            i++;
            indice2--;
        }


        int[] array2 = new int[sinpa.Count];

        for (int i = 0; i < array2.Length; i++)
        {
            array2[i] = sinpa[i];
        }
        Debug.Log("Arreglo sinpares: " + string.Join(", ", array2));


        List<int> listan = new List<int>();

        for (int i = 0; i < n; i++)
        {
            listan.Add(generador.Next(minimo, maximo));
        }

        Dictionary<int, int> diccionario = new Dictionary<int, int>();

        for (int i = minimo; i < maximo; i++)
        {
            diccionario.Add(i, 0);
        }


        for (int i = 0; i < listan.Count; i++)
        {
            int numero = listan[i];
            diccionario[numero]++; // suma 1 al valor de esa clave
        }


        foreach (KeyValuePair<int, int> par in diccionario)
        {
            Debug.Log("Número " + par.Key + ": aparece " + par.Value + " veces.");
        }

        int total = 0;

        foreach (KeyValuePair<int, int> par in diccionario)
        {
            total += par.Value;
        }

        Debug.Log("Total de números generados: " + total);




    }

}
