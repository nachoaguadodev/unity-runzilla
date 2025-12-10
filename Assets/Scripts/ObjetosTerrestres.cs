using UnityEngine;

public class ObjetosTerrestres : MonoBehaviour
{
    public GameObject[] obstaculos;

    public float tiempoMin = 1f;
    public float tiempoMax = 2f;

    // A qué distancia delante de la cámara aparecerán los obstáculos
    public float offsetX = 8f;

    private float contador;
    private Transform cam;   // referencia a la cámara

    void Start()
    {
        cam = Camera.main.transform;
        contador = Random.Range(tiempoMin, tiempoMax);
    }

    void Update()
    {
        // 1) Mover el Spawner para que vaya SIEMPRE delante de la cámara
        transform.position = new Vector3(
            cam.position.x + offsetX,   // un poco por delante
            transform.position.y,       // misma altura
            transform.position.z        // mismo Z
        );

        // 2) Lógica de tiempo para generar obstáculos
        contador -= Time.deltaTime;

        if (contador <= 0f)
        {
            GenerarObstaculo();
            contador = Random.Range(tiempoMin, tiempoMax);
        }
    }

    void GenerarObstaculo()
    {
        int indice = Random.Range(0, obstaculos.Length);
        Instantiate(obstaculos[indice], transform.position, Quaternion.identity);
    }
}
