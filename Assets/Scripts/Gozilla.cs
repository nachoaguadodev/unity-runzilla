using UnityEngine;

public class Gozilla : MonoBehaviour
{
    [Header("Salto")]
    public float fuerzaSalto = 15f;
    // Puedes ajustar la gravedad en el Rigidbody2D para que el salto se sienta bien
    // con la nueva perspectiva estática.

    private Rigidbody2D rb;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // IMPORTANTE: Asegúrate en el editor de Unity, en el Rigidbody2D, 
        // que en Constraints -> Freeze Position X esté marcado.
    }

    void Update()
    {
        // INPUT de salto
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }
    }

    void Saltar()
    {
        // Reseteamos la velocidad vertical actual para que el salto sea consistente
        // incluso si estamos bajando un poco una pendiente.
        rb.velocity = new Vector2(0f, rb.velocity.y); 
        
        // Aplicamos la fuerza hacia arriba
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        enSuelo = false;
    }

    // --- Detección de Suelo ---
    // Usar Tags es correcto. Asegúrate que tus objetos de suelo tengan el tag "Ground".
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            // Verificación extra opcional: asegurar que el contacto es por debajo del personaje
            // para evitar que salte si toca una pared lateral.
            if (collision.contacts[0].normal.y > 0.5f)
            {
                 enSuelo = true;
            }
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