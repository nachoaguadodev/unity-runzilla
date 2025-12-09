using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puntutacion : MonoBehaviour
{
    // Hacemos la puntuación float para acumular decimales, aunque mostremos enteros
    public float puntuacionActual = 0f;
    
    public TextMeshProUGUI textoPuntos;
    
    // Cuántos puntos te dan por cada unidad de distancia recorrida
    public float multiplicadorPuntos = 1f;      

    void Update()
    {
        // 1. Verificamos que el Manager de velocidad exista
        if (BGMOVEMENT.Instance == null) return;

        // 2. Calculamos cuánta distancia hemos "avanzado" en este frame
        // Fórmula: Distancia = Velocidad * Tiempo
        float velocidad = BGMOVEMENT.Instance.velocidadActual;
        float distanciaRecorridaEsteFrame = velocidad * Time.deltaTime;

        // 3. Sumamos esa distancia a la puntuación
        puntuacionActual += distanciaRecorridaEsteFrame * multiplicadorPuntos;

        // 4. Actualizamos el texto (quitando los decimales para que se vea limpio)
        if (textoPuntos != null)
        {
            // "F0" significa sin decimales. "D5" rellenaría con ceros (ej: 00123)
            textoPuntos.text = Mathf.FloorToInt(puntuacionActual).ToString();
        }
    }

    // Método extra por si quieres sumar puntos extra al coger monedas
    public void SumarPuntosExtra(int puntos)
    {
        puntuacionActual += puntos;
    }
}