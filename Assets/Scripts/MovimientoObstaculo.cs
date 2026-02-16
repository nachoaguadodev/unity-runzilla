using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    // Calculamos el ancho automáticamente para que el Spawner sepa cuánto esperar
    [HideInInspector] public float anchoReal;

    void Start()
    {
        // Intentamos obtener el Collider
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            anchoReal = col.size.x * transform.localScale.x;
        }
        else
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null) anchoReal = sr.bounds.size.x;
        }
    }

    void Update()
    {
        if (BGMOVEMENT.Instance == null) return;

        // Moverse a la izquierda a la velocidad del juego
        float velocidad = BGMOVEMENT.Instance.velocidadActual;
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        //Autodestrucción si sale de la pantalla
        if (transform.position.x < -25f)
        {
            Destroy(gameObject);
        }
    }
}
