using System.Collections;
using TMPro;
using UnityEngine;

public class Puntuacion : MonoBehaviour
{
    public static Puntuacion Instance;
    public float puntuacionActual = 0f;
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoRecord;
    // Esta variable guarda el récord que había antes de empezar a jugar 
    public float recordAlIniciar; 
    private float puntuacionMaxima;
    public TextMeshProUGUI textoBonus;
    public float multiplicadorPuntos = 1f;
    public AudioClip sonidoMoneda;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        if(textoBonus != null) textoBonus.gameObject.SetActive(false);

        //Cargamos el récord viejo
        puntuacionMaxima = PlayerPrefs.GetFloat("RecordGuardado", 0);
        
        //Guardamos una copia de ese récord para comparar luego
        recordAlIniciar = puntuacionMaxima;

        ActualizarTextoRecord();
    }

    void Update()
    {
        if (BGMOVEMENT.Instance == null) return;

        float velocidad = BGMOVEMENT.Instance.velocidadActual;
        if (velocidad > 0)
        {
            puntuacionActual += velocidad * Time.deltaTime * multiplicadorPuntos;
        }

        ActualizarUI();
        ComprobarRecord();
    }

    void ActualizarUI()
    {
        if (textoPuntos != null)
            textoPuntos.text = Mathf.FloorToInt(puntuacionActual).ToString();
    }

    void ComprobarRecord()
    {
        //Si superamos el récord actualizamosel número interno y guardamos
        if (puntuacionActual > puntuacionMaxima)
        {
            puntuacionMaxima = puntuacionActual;
            ActualizarTextoRecord();
            PlayerPrefs.SetFloat("RecordGuardado", puntuacionMaxima);
            PlayerPrefs.Save();
        }
    }

    void ActualizarTextoRecord()
    {
        if (textoRecord != null)
            textoRecord.text = "RECORD: " + Mathf.FloorToInt(puntuacionMaxima).ToString();
    }

    // FUNCIÓN QUE PREGUNTARÁ EL GAMEMANAGER
    public bool HaSuperadoRecord()
    {
        return puntuacionActual > recordAlIniciar;
    }

    public void SumarMoneda()
    {
        puntuacionActual += 100;
        if (sonidoMoneda != null) audioSource.PlayOneShot(sonidoMoneda);
        if(textoBonus != null) { StopAllCoroutines(); StartCoroutine(MostrarTextoBonus()); }
    }

    IEnumerator MostrarTextoBonus()
    {
        textoBonus.gameObject.SetActive(true);
        textoBonus.text = "+100";
        yield return new WaitForSeconds(1f);
        textoBonus.gameObject.SetActive(false);
    }
}