using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    public GameObject prefabMoneda;

    [Header("Ajustes de Tiempo")]
    public float tiempoMinimo = 1f;
    public float tiempoMaximo = 3f;

    [Header("Ajustes de Altura (Visuales)")]
    // Ajusta esto mirando las bolas de colores en la escena
    public float alturaSuelo = -3.5f; 
    public float alturaAire = -1.0f;

    private float cronometro;
    private float bordeDerechoCamara;

    void Start()
    {
        // Calculamos dónde termina la cámara por la derecha
        float altura = Camera.main.orthographicSize * 2f;
        float ancho = altura * Camera.main.aspect;
        bordeDerechoCamara = (ancho / 2f); // Borde derecho
        
        cronometro = Random.Range(tiempoMinimo, tiempoMaximo);
    }

    void Update()
    {
        cronometro -= Time.deltaTime;

        if (cronometro <= 0)
        {
            SpawnMoneda();
            cronometro = Random.Range(tiempoMinimo, tiempoMaximo);
        }
    }

    void SpawnMoneda()
    {
        // Elegir altura al azar (50% probabilidad)
        float alturaFinal = (Random.value > 0.5f) ? alturaSuelo : alturaAire;

        // POSICIÓN BLINDADA:
        // X = Borde de la cámara + 2 metros (para que no aparezca de golpe)
        // Y = La altura elegida
        // Z = -5 (Para asegurar que está delante del fondo)
        Vector3 posicion = new Vector3(
            Camera.main.transform.position.x + bordeDerechoCamara + 20f, 
            alturaFinal, 
            -0f 
        );

        Instantiate(prefabMoneda, posicion, Quaternion.identity);
    }

    // --- ESTO TE AYUDARÁ A VER LAS POSICIONES ---
    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        // Calculamos el borde derecho visualmente para el dibujo
        float h = Camera.main.orthographicSize * 2f;
        float w = h * Camera.main.aspect;
        float xDerecha = Camera.main.transform.position.x + (w / 2f) + 2f;

        // Dibujamos donde aparecerán las monedas
        // BOLA VERDE = Moneda de Suelo
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(xDerecha, alturaSuelo, 0), 0.5f);

        // BOLA AMARILLA = Moneda de Aire
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(xDerecha, alturaAire, 0), 0.5f);
    }
}