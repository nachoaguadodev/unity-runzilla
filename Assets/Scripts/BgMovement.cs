using UnityEngine;

// HUBIERA SIDO MEJOR RENOMBRAR EL ARCHIVO A "GameSpeedController", 
// PERO MANTENGO "BGMOVEMENT" PARA QUE COINCIDA CON TU ARCHIVO EXISTENTE.
public class BGMOVEMENT : MonoBehaviour
{
    public static BGMOVEMENT Instance; // Singleton para acceso fácil

    [Header("Configuración de Velocidad Global")]
    public float velocidadInicial = 5f;      // Velocidad al empezar
    public float velocidadMaxima = 15f;      // Máxima que quieres que alcance
    public float tiempoHastaMax = 60f;       // Segundos que tarda en llegar a la máxima

    [HideInInspector]
    public float velocidadActual;             // La velocidad a la que se mueve todo el mundo

    private float aceleracion;

    void Awake()
    {
        // Configuración básica del Singleton
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
        // Empezamos a la velocidad inicial
        velocidadActual = velocidadInicial;

        // Cálculo de la aceleración (igual que tenías antes)
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
        // Si el juego no está pausado (puedes añadir esa lógica luego), aumentamos la velocidad
        if (velocidadActual < velocidadMaxima)
        {
            velocidadActual += aceleracion * Time.fixedDeltaTime;
        }
    }

    // Método opcional si quieres detener todo al morir
    public void StopGame()
    {
        velocidadActual = 0;
        aceleracion = 0;
    }
}
