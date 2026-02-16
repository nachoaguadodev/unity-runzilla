using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    public GameObject prefabMoneda;
    public float tiempoMinimo = 1f;
    public float tiempoMaximo = 3f;
    public float alturaSuelo = -3.5f; 
    public float alturaAire = -1.0f;

    private float cronometro;
    private float bordeDerechoCamara;

    void Start()
    {
        float altura = Camera.main.orthographicSize * 2f;
        float ancho = altura * Camera.main.aspect;
        bordeDerechoCamara = (ancho / 2f);
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
        // Elegir altura al azar
        float alturaFinal = (Random.value > 0.5f) ? alturaSuelo : alturaAire;

        Vector3 posicion = new Vector3(
            Camera.main.transform.position.x + bordeDerechoCamara + 20f, 
            alturaFinal, 
            -0f 
        );

        Instantiate(prefabMoneda, posicion, Quaternion.identity);
    }

    //AYUDA PARA VER LAS POSICIONES
    private void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        float h = Camera.main.orthographicSize * 2f;
        float w = h * Camera.main.aspect;
        float xDerecha = Camera.main.transform.position.x + (w / 2f) + 2f;

        // BOLA VERDE = Moneda de Suelo
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(xDerecha, alturaSuelo, 0), 0.5f);

        // BOLA AMARILLA = Moneda de Aire
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(xDerecha, alturaAire, 0), 0.5f);
    }
}