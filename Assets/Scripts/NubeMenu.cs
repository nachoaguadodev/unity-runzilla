using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeMenu : MonoBehaviour
{
    public float velocidad = 2f; 
    public float intensidad = 0.1f; 
    private Vector3 escalaBase;
    private float semillaAleatoria; 

    void Start()
    {
        escalaBase = transform.localScale;
        semillaAleatoria = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Mathf.Sin nos da un valor que sube y baja suavemente entre -1 y 1.
        // Usamos Time.time para que se mueva con el tiempo
        float oscilacion = Mathf.Sin((Time.time + semillaAleatoria) * velocidad);

        // Calculamos cuánto vamos a cambiar el tamaño en este frame
        float factorCambio = oscilacion * intensidad;

        //Aplicamos efecto
        
        // Eje X Le sumamos el cambio -> se ensancha
        float nuevoX = escalaBase.x + factorCambio;

        // Eje Y Le RESTAMOS el cambio -> se aplana 
        float nuevoY = escalaBase.y - factorCambio;

        // Aplicamos la nueva escala al objeto
        transform.localScale = new Vector3(nuevoX, nuevoY, escalaBase.z);
    }
}
