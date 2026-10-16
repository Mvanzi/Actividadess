using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActividadSolid : MonoBehaviour, INavegador
{
    [Header("Referencias a las Vistas")]
    public VistaLogin vistaLogin;
    public VistaPerfil vistaPerfil;
    public VistaMenuAdmin vistaMenuAdmin;
    public VistaListaUsuarios vistaListaUsuarios;

    [Header("GameObjects de Paneles")]
    public GameObject panelLogin;
    public GameObject panelPerfil;
    public GameObject panelMenuAdmin;
    public GameObject panelListaUsuarios;

    private LoginPresenter loginPresenter;
    private PerfilPresenter perfilPresenter;
    private MenuAdminPresenter menuAdminPresenter;
    private ListaUsuariosPresenter listaUsuariosPresenter;

    void Start()
    {
        if (!ReferenciasCompletas())
        {
            enabled = false;
            return;
        }

        // 1. Mapper compartido, Repositorio y Servicio (Pasamos "this" para que la API pueda usar Corrutinas)
        UsuarioMapper mapper = new UsuarioMapper();
        IUserRepo repo = new ArchivoUserRepo(mapper);
        IApiUsuarios api = new RandomUserApi(this, mapper);

        // 2. Instanciamos los 4 Presentadores (inyectando dependencias)
        loginPresenter = new LoginPresenter(vistaLogin, repo, this);
        perfilPresenter = new PerfilPresenter(vistaPerfil, this);
        menuAdminPresenter = new MenuAdminPresenter(vistaMenuAdmin, api, repo, this);
        listaUsuariosPresenter = new ListaUsuariosPresenter(vistaListaUsuarios, repo, this);

        IrALogin();
    }

    // --- IMPLEMENTACIÓN DE INAVEGADOR ---
    public void IrALogin()
    {
        ApagarTodos();
        panelLogin.SetActive(true);
        loginPresenter.Inicializar();
    }

    public void IrAPerfil(Usuario usuario)
    {
        ApagarTodos();
        panelPerfil.SetActive(true);
        perfilPresenter.Inicializar(usuario);
    }

    public void IrAMenuAdmin()
    {
        ApagarTodos();
        panelMenuAdmin.SetActive(true);
    }

    public void IrAListaUsuarios()
    {
        ApagarTodos();
        panelListaUsuarios.SetActive(true);
        listaUsuariosPresenter.Inicializar();
    }

    private void ApagarTodos()
    {
        panelLogin.SetActive(false);
        panelPerfil.SetActive(false);
        panelMenuAdmin.SetActive(false);
        panelListaUsuarios.SetActive(false);
    }

    private bool ReferenciasCompletas()
    {
        bool ok = true;
        ok &= Verificar(vistaLogin, nameof(vistaLogin));
        ok &= Verificar(vistaPerfil, nameof(vistaPerfil));
        ok &= Verificar(vistaMenuAdmin, nameof(vistaMenuAdmin));
        ok &= Verificar(vistaListaUsuarios, nameof(vistaListaUsuarios));
        ok &= Verificar(panelLogin, nameof(panelLogin));
        ok &= Verificar(panelPerfil, nameof(panelPerfil));
        ok &= Verificar(panelMenuAdmin, nameof(panelMenuAdmin));
        ok &= Verificar(panelListaUsuarios, nameof(panelListaUsuarios));
        return ok;
    }

    private bool Verificar(UnityEngine.Object referencia, string nombre)
    {
        if (referencia != null) return true;
        Debug.LogError($"ActividadSolid: falta asignar '{nombre}' en el Inspector.", this);
        return false;
    }
}