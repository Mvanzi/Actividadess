using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UsuarioVistaMapper
{
    public static DatosUsuarioVista Crear(Usuario u)
    {
        return new DatosUsuarioVista(u.Nombre, u.Apellido, u.Edad, u.Telefono, u.Direccion, u.Email, u.FotoUrl);
    }
}

// Implementa IDestinoInicio: es el puente entre lo que pide el rol (modelo) y la navegación.
public class LoginPresenter : IDestinoInicio
{
    private readonly ILoginView vista;
    private readonly IUserRepo repo;
    private readonly INavegador navegador;

    public LoginPresenter(ILoginView vista, IUserRepo repo, INavegador nav)
    {
        this.vista = vista ?? throw new ArgumentNullException(nameof(vista));
        this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        navegador = nav ?? throw new ArgumentNullException(nameof(nav));

        this.vista.AlHacerLogin = IntentarLogin;
    }

    // La navegación pasa por el presentador; nadie toca la vista directamente.
    public void Inicializar()
    {
        vista.LimpiarCampos();
    }

    void IDestinoInicio.InicioNormal(Usuario usuario) { navegador.IrAPerfil(usuario); }
    void IDestinoInicio.InicioAdmin() { navegador.IrAMenuAdmin(); }

    private void IntentarLogin()
    {
        string username = vista.Username?.Trim();
        string password = vista.Password;

        // Validación de entrada (no es regla de autenticación)
        if (string.IsNullOrEmpty(username)) { vista.MostrarErrorUsuario("Ingrese un usuario."); return; }
        if (string.IsNullOrEmpty(password)) { vista.MostrarErrorPassword("Ingrese una contraseña."); return; }

        try
        {
            Usuario u = repo.Autenticar(username, password);

            // Navegación polimórfica (El rol decide a dónde ir, sin conocer la navegación)
            u.Rol.RedirigirInicio(this, u);
        }
        catch (UsuarioNoExisteException ex) { vista.MostrarErrorUsuario(ex.Message); }
        catch (ContraseniaIncorrectaException ex) { vista.MostrarErrorPassword(ex.Message); }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            vista.MostrarErrorGeneral("Ocurrió un error inesperado. Intente nuevamente.");
        }
    }
}