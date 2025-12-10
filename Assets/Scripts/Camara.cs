using UnityEngine;

public class Camara : MonoBehaviour
{
    // Ya no necesitamos target ni velocidades de seguimiento
    // porque la cámara se quedará FRENADA en el sitio.

    void Start()
    {
        // Opcional: Forzar la posición al iniciar para asegurar que siempre está igual
        // transform.position = new Vector3(0, 0, -10); 
    }

    void Update()
    {
        // VACÍO. La cámara no debe moverse.
        // El movimiento lo hace el FONDO y el SUELO.
    }
}