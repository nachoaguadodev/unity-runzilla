using UnityEngine;

public class MonedaSegura : MonoBehaviour
{
    private bool cogida = false;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) 
        {
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