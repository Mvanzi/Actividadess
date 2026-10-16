using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestinoInicio
{
    void InicioNormal(Usuario usuario);
    void InicioAdmin();
}

public interface IRol
{
    // Polimorfismo en lugar de "if (rol is RolAdmin)" en el presentador
    bool VisibleEnListado { get; }
    void RedirigirInicio(IDestinoInicio destino, Usuario usuario);
}

public class RolNormal : IRol
{
    public bool VisibleEnListado => true;

    public void RedirigirInicio(IDestinoInicio destino, Usuario usuario)
    {
        destino.InicioNormal(usuario);
    }
}

public class RolAdmin : IRol
{
    public bool VisibleEnListado => false;

    public void RedirigirInicio(IDestinoInicio destino, Usuario usuario)
    {
        destino.InicioAdmin();
    }
}