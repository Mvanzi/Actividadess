using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAdminPresenter
{
    public MenuAdminPresenter(IMenuAdminView vista, IApiUsuarios api, IUserRepo repo, INavegador nav)
    {
        if (vista == null) throw new ArgumentNullException(nameof(vista));
        if (api == null) throw new ArgumentNullException(nameof(api));
        if (repo == null) throw new ArgumentNullException(nameof(repo));
        if (nav == null) throw new ArgumentNullException(nameof(nav));

        vista.AlSolicitarVerUsuarios = () => nav.IrAListaUsuarios();

        vista.AlSolicitarGenerarNuevos = () =>
        {
            api.DescargarNuevosUsuariosAPI(
                nuevos =>
                {
                    try
                    {
                        repo.ReemplazarUsuarios(nuevos);
                        nav.IrAListaUsuarios();
                    }
                    catch (ErrorPersistenciaException ex) { vista.MostrarError(ex.Message); }
                    catch (ArgumentException ex) { vista.MostrarError(ex.Message); }
                },
                mensajeError => vista.MostrarError(mensajeError));
        };
    }
}