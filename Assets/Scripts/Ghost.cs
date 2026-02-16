using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float velocidadExtra = 3f;
    public float coordenadaDestruccionX = -20f; 

    void Update()
    {
        Moverse();
        VerificarSalida();
    }

    void Moverse()
    {
        float velocidadMundo = 0f;
        
        // Sincronizamos con el suelo
        if (BGMOVEMENT.Instance != null)
        {
            velocidadMundo = BGMOVEMENT.Instance.velocidadActual;
        }

        float velocidadTotal = velocidadMundo + velocidadExtra;
        transform.Translate(Vector3.left * velocidadTotal * Time.deltaTime);
    }

    void VerificarSalida()
    {
        if (transform.position.x < coordenadaDestruccionX)
        {
            Destroy(gameObject);
        }
    }
}