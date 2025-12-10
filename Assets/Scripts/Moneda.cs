using UnityEngine;

public class MonedaSegura : MonoBehaviour
{
    private bool cogida = false;

    void Start()
    {
        // ESTO ES VITAL AHORA QUE LA Z ES 4:
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) 
        {
            // Aunque esté en Z=4 (atrás), esto le dice a Unity:
            // "Píntame encima de todo lo que tenga orden menor a 20"
            sr.sortingOrder = 20; 
        }
    }

    void Update()
    {
        if (BGMOVEMENT.Instance == null) return;

        float velocidad = BGMOVEMENT.Instance.velocidadActual;
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        if (transform.position.x < -30f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cogida) return;

        if (collision.CompareTag("Player"))
        {
            cogida = true;
            if (Puntuacion.Instance != null)
            {
                Puntuacion.Instance.SumarMoneda();
            }
            Destroy(gameObject);
        }
    }
}