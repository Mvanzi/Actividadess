using System.IO;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

// 1. LA INTERFAZ (ISP)
public interface ILoginView
{
    void MostrarErrorUsuario(string mensaje);
    void MostrarErrorPassword(string mensaje);
    void MostrarPantallaUsuario(UsuarioData usuario);
    void MostrarPantallaLogin();
}


public class ArchivoUsuarioModel //MODELO
{
    public RespuestaAPI CargarUsuarios()
    {
        string ruta = Path.Combine(Application.streamingAssetsPath, "usuarios.json");

        if (File.Exists(ruta))
        {
            string contenidoJson = File.ReadAllText(ruta);
            return JsonUtility.FromJson<RespuestaAPI>(contenidoJson);
        }

        Debug.LogError("No se encontró el archivo usuarios.json en StreamingAssets.");
        return null;
    }
}


public class LoginPresenter //PRESENTADOR
{
    private ILoginView vista;
    private ArchivoUsuarioModel modelo;
    private RespuestaAPI baseDeDatos;

    public LoginPresenter(ILoginView vista, ArchivoUsuarioModel modelo)
    {
        this.vista = vista;
        this.modelo = modelo;
        this.baseDeDatos = modelo.CargarUsuarios(); // Carga el archivo al iniciar
    }

    public void IntentarLogin(string username, string password)
    {
        if (baseDeDatos == null || baseDeDatos.results == null) return;

        UsuarioData usuarioEncontrado = null;
        foreach (var u in baseDeDatos.results)
        {
            if (u.login.username == username)
            {
                usuarioEncontrado = u;
                break;
            }
        }

        if (usuarioEncontrado == null)
        {
            vista.MostrarErrorUsuario("El usuario no existe.");
        }
        else if (usuarioEncontrado.login.password != password)
        {
            vista.MostrarErrorPassword("La contraseña es incorrecta.");
        }
        else
        {
            vista.MostrarPantallaUsuario(usuarioEncontrado);
        }
    }
}


public class ActividadMVP : MonoBehaviour, ILoginView //VISTA
{
    [Header("UI Login")]
    public GameObject panelLogin;
    public TMP_InputField inputUsuario;
    public TMP_InputField inputPassword;
    public TextMeshProUGUI textoErrorUsuario;
    public TextMeshProUGUI textoErrorPassword;

    [Header("UI Usuario")]
    public GameObject panelUsuario;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoApellido;
    public TextMeshProUGUI textoEdad;
    public TextMeshProUGUI textoTelefono;
    public TextMeshProUGUI textoDireccion;
    public TextMeshProUGUI textoEmail;
    public Image imagenPerfil;

    private LoginPresenter presentador;

    void Start()
    {
        presentador = new LoginPresenter(this, new ArchivoUsuarioModel());
        MostrarPantallaLogin();
    }

    void Update()
    {
        // Si presionamos Tab y el cursor está en el input de usuario, saltamos a la contraseña
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputUsuario.isFocused)
            {
                inputPassword.Select();
            }
        }
    }

    // Este método lo asignás al botón "Login"
    public void BotonLoginPresionado()
    {
        textoErrorUsuario.gameObject.SetActive(false);
        textoErrorPassword.gameObject.SetActive(false);

        presentador.IntentarLogin(inputUsuario.text, inputPassword.text);
    }

    public void MostrarErrorUsuario(string mensaje)
    {
        textoErrorUsuario.text = mensaje;
        textoErrorUsuario.gameObject.SetActive(true);
    }

    public void MostrarErrorPassword(string mensaje)
    {
        textoErrorPassword.text = mensaje;
        textoErrorPassword.gameObject.SetActive(true);
    }

    public void MostrarPantallaLogin()
    {
        panelUsuario.SetActive(false);
        panelLogin.SetActive(true);

        textoErrorUsuario.gameObject.SetActive(false);
        textoErrorPassword.gameObject.SetActive(false);

        inputUsuario.text = "";
        inputPassword.text = "";
    }

    public void MostrarPantallaUsuario(UsuarioData usuario)
    {
        panelLogin.SetActive(false);
        panelUsuario.SetActive(true);

        textoNombre.text = "Nombre: " + usuario.name.first;
        textoApellido.text = "Apellido: " + usuario.name.last;
        textoEdad.text = "Edad: " + usuario.dob.age;
        textoTelefono.text = "Teléfono: " + usuario.phone;
        textoEmail.text = "Email: " + usuario.email;

        textoDireccion.text = "Dirección:\n" + usuario.location.street.name + " " +
                              usuario.location.street.number + ",\n" +
                              usuario.location.city + ", " + usuario.location.country;

        StartCoroutine(DescargarImagen(usuario.picture.large));
    }

    private IEnumerator DescargarImagen(string url)
    {
        string rutaFinal = url;

        // Si el texto no empieza con "http", armamos la ruta local hacia StreamingAssets
        if (!url.StartsWith("http"))
        {
            rutaFinal = "file://" + Path.Combine(Application.streamingAssetsPath, url);
        }

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(rutaFinal);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            imagenPerfil.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogError("No se pudo cargar la imagen: " + www.error);
        }
    }
}


[System.Serializable]
public class RespuestaAPI { public UsuarioData[] results; } //DATOS

[System.Serializable]
public class UsuarioData
{
    public LoginData login;
    public NameData name;
    public LocationData location;
    public string email;
    public DobData dob;
    public string phone;
    public PictureData picture;
}

[System.Serializable]
public class LoginData { public string username; public string password; }

[System.Serializable]
public class NameData { public string first; public string last; }

[System.Serializable]
public class LocationData
{
    public StreetData street;
    public string city;
    public string state;
    public string country;
}

[System.Serializable]
public class StreetData { public int number; public string name; }

[System.Serializable]
public class DobData { public int age; }

[System.Serializable]
public class PictureData { public string large; }