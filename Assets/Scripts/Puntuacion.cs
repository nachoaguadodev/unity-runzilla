using System.Collections;
using TMPro;
using UnityEngine;

public class Puntuacion : MonoBehaviour
{
    public static Puntuacion Instance; // Singleton para acceder fácil desde la moneda

    public float puntuacionActual = 0f;
    public TextMeshProUGUI textoPuntos;
    
    [Header("Bonus Moneda")]
    public TextMeshProUGUI textoBonus; // Arrastra aquí el texto que dice "+100"
    public float multiplicadorPuntos = 1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        // Al empezar, escondemos el texto de bonus
        if(textoBonus != null) textoBonus.gameObject.SetActive(false);
    }

    void Update()
    {
        if (BGMOVEMENT.Instance == null) return;

        // Puntuación por distancia
        float velocidad = BGMOVEMENT.Instance.velocidadActual;
        puntuacionActual += velocidad * Time.deltaTime * multiplicadorPuntos;

        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntos != null)
            textoPuntos.text = Mathf.FloorToInt(puntuacionActual).ToString();
    }

    // --- ESTA ES LA FUNCIÓN NUEVA ---
    public void SumarMoneda()
    {
        // 1. Sumar 100 puntos
        puntuacionActual += 100;
        
        // 2. Mostrar efecto visual
        if(textoBonus != null)
        {
            StopAllCoroutines(); // Si ya había uno activo, lo reseteamos
            StartCoroutine(MostrarTextoBonus());
        }
        
        // Aquí podrías poner un sonido: AudioSource.PlayOneShot(sonidoMoneda);
    }

    IEnumerator MostrarTextoBonus()
    {
        textoBonus.gameObject.SetActive(true);
        textoBonus.text = "+100"; // Aseguramos que diga 100
        
        // Esperamos 1 segundo
        yield return new WaitForSeconds(1f);
        
        textoBonus.gameObject.SetActive(false);
    }
}