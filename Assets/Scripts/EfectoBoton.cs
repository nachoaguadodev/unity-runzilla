using UnityEngine;
using UnityEngine.EventSystems; // Necesario para detectar el ratón

public class EfectoBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Configuración Visual")]
    public float escalaAlCrecer = 1.1f;
    
    [Header("Configuración de Audio")]
    public AudioClip sonidoSelect; // Arrastra aquí sonidoMenuSelect
    public AudioClip sonidoClick;  // Arrastra aquí sonidoMenuOK
    [Range(0, 1)] public float volumen = 0.5f; // Volumen al 50%

    private Vector3 escalaOriginal;
    private RectTransform miRectTransform;
    private AudioSource miAudioSource;

    void Start()
    {
        miRectTransform = GetComponent<RectTransform>();
        escalaOriginal = miRectTransform.localScale;
        // Buscamos si el botón ya tiene un AudioSource.
        // Si no lo tiene, se lo ponemos nosotros automáticamente por código
        miAudioSource = GetComponent<AudioSource>();
        if (miAudioSource == null)
        {
            miAudioSource = gameObject.AddComponent<AudioSource>();
        }
        // Configuramos el altavoz para que no haga eco ni suene al iniciar
        miAudioSource.playOnAwake = false;
    }

    //HOVER
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Efecto visual
        miRectTransform.localScale = escalaOriginal * escalaAlCrecer;

        // Efecto sonoro
        if (sonidoSelect != null)
        {
            // PlayOneShot permite que los sonidos se solapen si pasas muy rápido
            miAudioSource.PlayOneShot(sonidoSelect, volumen);
        }
    }

    //EFECTO AL QUITAR HOVER
    public void OnPointerExit(PointerEventData eventData)
    {
        miRectTransform.localScale = escalaOriginal;
    }

    //AL HACER CLIC
    public void OnPointerClick(PointerEventData eventData)
    {
        if (sonidoClick != null)
        {
            miAudioSource.PlayOneShot(sonidoClick, volumen);
        }
    }

    void OnDisable()
    {
        if (miRectTransform != null) miRectTransform.localScale = escalaOriginal;
    }
}