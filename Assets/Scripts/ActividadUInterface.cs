using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class Actividad : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoApellido;
    public TextMeshProUGUI textoEdad;
    public TextMeshProUGUI textoTelefono;
    public TextMeshProUGUI textoDireccion;
    public TextMeshProUGUI textoEmail;
    public Image imagenPerfil;

    public Button botonAnterior;
    public Button botonSiguiente;
    private UsuarioData[] listaUsuarios;
    private int indiceActual = 0;

    void Start()
    {
        botonAnterior.interactable = false;
        botonSiguiente.interactable = false;

        StartCoroutine(GetUser());
    }

    IEnumerator GetUser()
    {
        
        UnityWebRequest www = UnityWebRequest.Get("https://randomuser.me/api/?results=10&nat=us,es,gb");
        yield return www.SendWebRequest(); 

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string result = www.downloadHandler.text;
            RespuestaAPI datosParseados = JsonUtility.FromJson<RespuestaAPI>(result);

            listaUsuarios = datosParseados.results;
            indiceActual = 0;
            ActualizarUI();
        }
    }

    void ActualizarUI()
    {
        UsuarioData usuarioActual = listaUsuarios[indiceActual];

        textoNombre.text = "Nombre: " + usuarioActual.name.first;
        textoApellido.text = "Apellido: " + usuarioActual.name.last;
        textoEdad.text = "Edad: " + usuarioActual.dob.age.ToString();
        textoTelefono.text = "Teléfono: " + usuarioActual.phone;
        textoEmail.text = "Email: " + usuarioActual.email;

        string direccionCompleta = usuarioActual.location.street.name + " " +
                                   usuarioActual.location.street.number + ", " +
                                   usuarioActual.location.city + ", " +
                                   usuarioActual.location.state + ", " +
                                   usuarioActual.location.country;

        textoDireccion.text = "Dirección:\n" + direccionCompleta;

        StartCoroutine(GetTexture(usuarioActual.picture.large));

        botonAnterior.interactable = (indiceActual > 0);
        botonSiguiente.interactable = (indiceActual < listaUsuarios.Length - 1);
    }

    public void SiguienteUsuario()
    {
        if (indiceActual < listaUsuarios.Length - 1)
        {
            indiceActual++;
            ActualizarUI();
        }
    }

    public void AnteriorUsuario()
    {
        if (indiceActual > 0)
        {
            indiceActual--;
            ActualizarUI();
        }
    }

    IEnumerator GetTexture(string urlImagen)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(urlImagen);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            imagenPerfil.sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    [System.Serializable]
    public class RespuestaAPI
    {
        public UsuarioData[] results;
    }

    [System.Serializable]
    public class UsuarioData
    {
        public NameData name;
        public LocationData location;
        public string email;
        public DobData dob;
        public string phone;
        public PictureData picture;
    }

    [System.Serializable]
    public class NameData
    {
        public string first;
        public string last;
    }

    [System.Serializable]
    public class LocationData
    {
        public StreetData street;
        public string city;
        public string state;
        public string country;
    }

    [System.Serializable]
    public class StreetData
    {
        public int number;
        public string name;
    }

    [System.Serializable]
    public class DobData
    {
        public int age;
    }

    [System.Serializable]
    public class PictureData
    {
        public string large;
    }
}