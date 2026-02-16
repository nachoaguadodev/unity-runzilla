using UnityEngine;
using System.Collections.Generic;

public class InfiniteLayer : MonoBehaviour
{
    public float anchoPieza = 19.2f;
    public float limiteIzquierda = -25f;
    public float factorVelocidad = 1f;
    public List<Transform> piezas; 

    void Start()
    {
        //Llenamos la lista con los hijos que tenga este objeto
        piezas = new List<Transform>();
        foreach (Transform hijo in transform)
        {
            piezas.Add(hijo);
        }

        if (piezas.Count < 2)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        if (BGMOVEMENT.Instance == null) return;

        float velocidad = BGMOVEMENT.Instance.velocidadActual * factorVelocidad;
        
        foreach(Transform pieza in piezas)
        {
            pieza.Translate(Vector3.left * velocidad * Time.deltaTime);
        }

        // Reciclamos el primero
        Transform primeraPieza = piezas[0];

        if (primeraPieza.position.x < limiteIzquierda)
        {
            MandarAlFinal(primeraPieza);
        }
    }

    void MandarAlFinal(Transform piezaAMover)
    {
        Transform ultimaPieza = piezas[piezas.Count - 1];

        Vector3 nuevaPos = ultimaPieza.position;
        nuevaPos.x += anchoPieza; 

        piezaAMover.position = nuevaPos;

        piezas.RemoveAt(0);
        piezas.Add(piezaAMover);
    }
}
