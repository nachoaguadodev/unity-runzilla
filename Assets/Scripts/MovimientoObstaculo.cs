using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        // Mover hacia la izquierda
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        // Si el obstáculo sale por la izquierda de la pantalla, lo destruimos
        if (transform.position.x < -10f) // Ajusta el -10 según tu escena
        {
            Destroy(gameObject);
        }
    }
}
