using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcesadorDeVentas
{
    public double CalcularTotal(List<double> precios, ICalculadorImpuesto calculador, bool esClienteFrecuente)
    {
        double total = 0;

        foreach (double precio in precios)
        {
            if (precio > 0)
            {
                total += calculador.Calcular(precio);
            }
        }

        if (esClienteFrecuente)
        {
            total *= 0.85;
        }

        return total;
    }
}

public interface ICalculadorImpuesto
{
    double Calcular(double precioBase);
}

public class ImpuestoNormal : ICalculadorImpuesto
{
    public double Calcular(double precioBase)
    {
        return precioBase * 1.21;
    }
}

public class ImpuestoReducido : ICalculadorImpuesto
{
    public double Calcular(double precioBase)
    {
        return precioBase * 1.105;
    }
}

public class SinImpuesto : ICalculadorImpuesto
{
    public double Calcular(double precioBase)
    {
        return precioBase;
    }
}


List<double> carritoDeCompras = new List<double> { 100, 50, 200 };

ICalculadorImpuesto tipoDeImpuesto = new ImpuestoNormal();

bool esFrecuente = true;

ProcesadorDeVentas procesador = new ProcesadorDeVentas();

double totalAPagar = procesador.CalcularTotal(carritoDeCompras, tipoDeImpuesto, esFrecuente);