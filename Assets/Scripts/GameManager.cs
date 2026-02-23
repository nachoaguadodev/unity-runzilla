using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject panelGameOver;
    public TextMeshProUGUI textoPuntuacionFinal;
    public GameObject textoPuntuacionJuego;
    public GameObject cartelNuevoRecord; 
    public AudioSource musicaDeFondo;
    public AudioClip clipGameOver;
    private AudioSource audioSourceGM;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Application.targetFrameRate = 120;
        audioSourceGM = GetComponent<AudioSource>();
        if (audioSourceGM == null) audioSourceGM = gameObject.AddComponent<AudioSource>();

        Time.timeScale = 1f;
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (cartelNuevoRecord != null) cartelNuevoRecord.SetActive(false);
        if (textoPuntuacionJuego != null) textoPuntuacionJuego.SetActive(true);
    }

    public void ActivarGameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (textoPuntuacionJuego != null) textoPuntuacionJuego.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(true);

        if (Puntuacion.Instance != null && textoPuntuacionFinal != null)
        {
            int puntosFinales = Mathf.FloorToInt(Puntuacion.Instance.puntuacionActual);
            textoPuntuacionFinal.text = "PUNTUACIÓN: " + puntosFinales.ToString();
        }

        //LÓGICA DE NUEVO RÉCORD
        if (Puntuacion.Instance != null && Puntuacion.Instance.HaSuperadoRecord())
        {
            if (cartelNuevoRecord != null) 
            {
                cartelNuevoRecord.SetActive(true);
            }
        }
        else
        {
            if (cartelNuevoRecord != null) cartelNuevoRecord.SetActive(false);
        }

        //AUDIO
        if (musicaDeFondo != null) musicaDeFondo.Stop(); 
        
        if (clipGameOver != null && audioSourceGM != null)
        {
            audioSourceGM.PlayOneShot(clipGameOver);
        }

        Time.timeScale = 0f;
    }

    public void BotonReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotonIrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}