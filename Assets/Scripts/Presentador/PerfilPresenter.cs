using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfilPresenter
{
    private readonly IPerfilView vista;

    public PerfilPresenter(IPerfilView vista, INavegador nav)
    {
        this.vista = vista ?? throw new ArgumentNullException(nameof(vista));
        if (nav == null) throw new ArgumentNullException(nameof(nav));

        this.vista.AlHacerLogout = () => nav.IrALogin();
    }

    public void Inicializar(Usuario usuario)
    {
        if (usuario == null) throw new ArgumentNullException(nameof(usuario));
        vista.MostrarDatosPerfil(UsuarioVistaMapper.Crear(usuario));
    }
}
