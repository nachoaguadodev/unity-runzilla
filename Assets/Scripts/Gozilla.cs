using UnityEngine;

public class Gozilla : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadInicial = 5f;      // Velocidad al empezar
    public float velocidadMaxima = 15f;      // Máxima que quieres que alcance
    public float tiempoHastaMax = 60f;       // Segundos que tarda en llegar a la máxima

    [Header("Salto")]
    public float fuerzaSalto = 15f;

    private float velocidad;                 // Velocidad actual
    private float aceleracion;               // Calculada en Start
    private Rigidbody2D rb;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Empezamos a la velocidad inicial
        velocidad = velocidadInicial;

        // Aceleración: de velocidadInicial a velocidadMaxima en "tiempoHastaMax" segundos
        if (tiempoHastaMax > 0)
        {
            aceleracion = (velocidadMaxima - velocidadInicial) / tiempoHastaMax;
        }
        else
        {
            aceleracion = 0f;
        }
    }

    void Update()
    {
        // INPUT de salto aquí para no perder pulsaciones
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            enSuelo = false;
        }
    }

    void FixedUpdate()
    {
        // Aumentar velocidad de forma SUAVE y limitada
        if (velocidad < velocidadMaxima)
        {
            velocidad += aceleracion * Time.fixedDeltaTime;
        }

        // Movimiento constante hacia la derecha
        rb.velocity = new Vector2(velocidad, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            enSuelo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            enSuelo = false;
        }
    }
}