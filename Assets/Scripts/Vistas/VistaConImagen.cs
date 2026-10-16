using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public abstract class VistaConImagen : MonoBehaviour
{
    public Image imagenPerfil;

    private Coroutine descargaActual;
    private Texture2D texturaActual;
    private Sprite spriteActual;

    protected void MostrarImagen(string url)
    {
        CancelarDescarga();
        if (imagenPerfil == null || string.IsNullOrWhiteSpace(url)) return;
        descargaActual = StartCoroutine(DescargarImagen(url));
    }

    protected void LimpiarImagen()
    {
        CancelarDescarga();
        LiberarImagenAnterior();
        if (imagenPerfil != null) imagenPerfil.sprite = null;
    }

    private void CancelarDescarga()
    {
        if (descargaActual != null)
        {
            StopCoroutine(descargaActual);
            descargaActual = null;
        }
    }

    private IEnumerator DescargarImagen(string url)
    {
        string rutaFinal = url.StartsWith("http") ? url : "file://" + Path.Combine(Application.streamingAssetsPath, url);

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(rutaFinal))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("No se pudo cargar la imagen: " + www.error);
                descargaActual = null;
                yield break;
            }

            LiberarImagenAnterior();
            texturaActual = DownloadHandlerTexture.GetContent(www);
            spriteActual = Sprite.Create(
                texturaActual,
                new Rect(0, 0, texturaActual.width, texturaActual.height),
                new Vector2(0.5f, 0.5f));
            imagenPerfil.sprite = spriteActual;
        }
        descargaActual = null;
    }

    private void LiberarImagenAnterior()
    {
        if (spriteActual != null) Destroy(spriteActual);
        if (texturaActual != null) Destroy(texturaActual);
        spriteActual = null;
        texturaActual = null;
    }

    protected virtual void OnDestroy()
    {
        LiberarImagenAnterior();
    }
}