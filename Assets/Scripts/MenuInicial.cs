using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para las Corutinas

public class MenuInicial : MonoBehaviour
{
    [Header("Configuración de la Transición")]
    public CanvasGroup cortinilla; //Panel negro para no cargar juego de golpe
    public float velocidadFundido = 1f;

    public void Jugar()
    {
        // En vez de cargar ya, iniciamos la Corutina
        StartCoroutine(HacerFundidoYCambiar());
    }

    public void Salir()
    {
        // Eso solo funcionará si hacemos el .exe
        Debug.Log("Saliendo...");
        Application.Quit();
    }

    IEnumerator HacerFundidoYCambiar()
    {
        //Bloqueamos los clics para que el jugador no pueda pulsar nada más
        cortinilla.blocksRaycasts = true;

        //Bucle para aumentar la opacidad poco a poco
        float tiempo = 0;
        while (tiempo < 1)
        {
            // Aumentamos el contador
            tiempo += Time.deltaTime / velocidadFundido;
            
            // y aplicamos la transparencia
            cortinilla.alpha = tiempo;

            // Esperamos al siguiente frame
            yield return null;
        }

        // Cargamos
        SceneManager.LoadScene("SampleScene"); 
    }
}