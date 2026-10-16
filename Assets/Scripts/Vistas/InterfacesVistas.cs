using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoginView
{
    string Username { get; }
    string Password { get; }
    Action AlHacerLogin { get; set; }
    void MostrarErrorUsuario(string mensaje);
    void MostrarErrorPassword(string mensaje);
    void MostrarErrorGeneral(string mensaje);
    void LimpiarCampos();
}

public interface IPerfilView
{
    Action AlHacerLogout { get; set; }
    void MostrarDatosPerfil(DatosUsuarioVista datos);
}

public interface IMenuAdminView
{
    Action AlSolicitarVerUsuarios { get; set; }
    Action AlSolicitarGenerarNuevos { get; set; }
    void MostrarError(string mensaje);
}
public interface IListaUsuariosView
{
    Action AlHacerSiguiente { get; set; }
    Action AlHacerAnterior { get; set; }
    Action AlHacerLogout { get; set; }
    void MostrarDatosUsuario(DatosUsuarioVista datos);
    void MostrarSinUsuarios();
    void DeshabilitarBotonAnterior();
    void DeshabilitarBotonSiguiente();
    void HabilitarBotonAnterior();
    void HabilitarBotonSiguiente();
}

