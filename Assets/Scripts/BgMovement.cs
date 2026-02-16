using UnityEngine;

public class BGMOVEMENT : MonoBehaviour
{
    public static BGMOVEMENT Instance; // Singleton

    [Header("Configuración de Velocidad Global")]
    public float velocidadInicial = 5f;      // Velocidad al empezar
    public float velocidadMaxima = 15f;      // Máxima que queremos que alcance
    public float tiempoHastaMax = 60f;       // Segundos que tarda en llegar a la máxima

    [HideInInspector]
    public float velocidadActual;             // La velocidad a la que se mueve todo el mundo

    private float aceleracion;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        velocidadActual = velocidadInicial;

        if (tiempoHastaMax > 0)
        {
            aceleracion = (velocidadMaxima - velocidadInicial) / tiempoHastaMax;
        }
        else
        {
            aceleracion = 0f;
        }
    }

    void FixedUpdate()
    {
        if (velocidadActual < velocidadMaxima)
        {
            velocidadActual += aceleracion * Time.fixedDeltaTime;
        }
    }

    public void StopGame()
    {
        velocidadActual = 0;
        aceleracion = 0;
    }
}
