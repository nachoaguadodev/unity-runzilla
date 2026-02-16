using UnityEngine;

public class EfectoLatidoUI : MonoBehaviour
{
    public float velocidad = 4f; 

    public float intensidad = 0.1f; 

    private Vector3 escalaOriginal;

    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        float oscilacion = Mathf.Sin(Time.unscaledTime * velocidad);

        // Calculamos el factor de cambio
        float factor = oscilacion * intensidad;

        // Si X crece, Y decrece y viceversa
        float nuevoX = escalaOriginal.x + factor;
        float nuevoY = escalaOriginal.y - factor;

        // Aplicamos la escala
        transform.localScale = new Vector3(nuevoX, nuevoY, escalaOriginal.z);
    }
}