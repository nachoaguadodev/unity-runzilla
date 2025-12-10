using UnityEngine;

public class Gozilla : MonoBehaviour
{
    [Header("Configuración Salto")]
    public float fuerzaSalto = 15f;
    public float longitudLaser = 1.1f; // Cuánto mide el láser hacia abajo
    public LayerMask capaSuelo;        // Qué capas se consideran "suelo"

    private Rigidbody2D rb;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. LANZAR LÁSER (RAYCAST)
        // Disparamos un rayo invisible desde el centro del personaje hacia abajo
        // Si toca algo de la capa "capaSuelo", devuelve true.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudLaser, capaSuelo);
        
        if (hit.collider != null)
        {
            enSuelo = true;
        }
        else
        {
            enSuelo = false;
        }

        // 2. INPUT DE SALTO
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            Saltar();
        }
    }

    void Saltar()
    {
        // Reseteamos la velocidad Y para que el salto sea siempre igual de potente
        // aunque estemos cayendo un poco.
        rb.velocity = new Vector2(0, 0); 
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }

    // DIBUJO VISUAL DEL LÁSER (Para que veas si toca el suelo)
    private void OnDrawGizmos()
    {
        // Si toca suelo: Verde. Si está en el aire: Rojo.
        if (enSuelo) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;

        // Dibuja la línea
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudLaser);
    }
}