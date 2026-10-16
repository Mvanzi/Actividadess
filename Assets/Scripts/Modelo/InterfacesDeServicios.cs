using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserRepo
{
    Usuario Autenticar(string username, string password);
    List<Usuario> ObtenerTodos();
    void ReemplazarUsuarios(List<Usuario> nuevos);
}

public interface IApiUsuarios
{
    void DescargarNuevosUsuariosAPI(Action<List<Usuario>> onExito, Action<string> onError);
}

public interface INavegador
{
    void IrALogin();
    void IrAPerfil(Usuario usuario);
    void IrAMenuAdmin();
    void IrAListaUsuarios();
}