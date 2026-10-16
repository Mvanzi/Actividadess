using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaUsuariosPresenter
{
    private readonly IListaUsuariosView vista;
    private readonly IUserRepo repo;
    private List<Usuario> usuarios = new List<Usuario>();
    private int indice;

    public ListaUsuariosPresenter(IListaUsuariosView vista, IUserRepo repo, INavegador nav)
    {
        this.vista = vista ?? throw new ArgumentNullException(nameof(vista));
        this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        if (nav == null) throw new ArgumentNullException(nameof(nav));

        this.vista.AlHacerAnterior = Anterior;
        this.vista.AlHacerSiguiente = Siguiente;
        this.vista.AlHacerLogout = () => nav.IrALogin();
    }

    public void Inicializar()
    {
        usuarios = repo.ObtenerTodos();
        usuarios.RemoveAll(u => !u.Rol.VisibleEnListado); // El rol decide si se lista (sin chequeo de tipo)
        indice = 0;
        ActualizarVista();
    }

    private void Anterior() { if (indice > 0) { indice--; ActualizarVista(); } }
    private void Siguiente() { if (indice < usuarios.Count - 1) { indice++; ActualizarVista(); } }

    private void ActualizarVista()
    {
        if (usuarios.Count == 0)
        {
            vista.MostrarSinUsuarios();
            return;
        }

        vista.MostrarDatosUsuario(UsuarioVistaMapper.Crear(usuarios[indice]));

        if (indice == 0) vista.DeshabilitarBotonAnterior(); else vista.HabilitarBotonAnterior();
        if (indice >= usuarios.Count - 1) vista.DeshabilitarBotonSiguiente(); else vista.HabilitarBotonSiguiente();
    }
}