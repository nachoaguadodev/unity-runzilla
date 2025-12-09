using UnityEngine;
using System.Collections.Generic;

public class InfiniteLayer : MonoBehaviour
{
    [Header("Configuración Manual")]
    [Tooltip("El ancho EXACTO de una de tus piezas (Sprite).")]
    public float anchoPieza = 19.2f;

    [Tooltip("Coordenada X donde la pieza desaparece y se va al final.")]
    public float limiteIzquierda = -25f;

    [Tooltip("Pon 1 para el suelo. Pon menos (ej: 0.5) para fondos lentos.")]
    public float factorVelocidad = 1f;

    [Header("Referencias (Automático)")]
    public List<Transform> piezas; // La lista de tus objetos (Suelos o Fondos)

    void Start()
    {
        // 1. Llenamos la lista con los hijos que tenga este objeto
        piezas = new List<Transform>();
        foreach (Transform hijo in transform)
        {
            piezas.Add(hijo);
        }

        // 2. Validación: Necesitas al menos 2 piezas (idealmente 3)
        if (piezas.Count < 2)
        {
            Debug.LogError("¡Necesitas poner al menos 2 o 3 objetos hijos para que el bucle funcione!");
            this.enabled = false;
        }
    }

    void Update()
    {
        // Si no hay GameManager, no nos movemos
        if (BGMOVEMENT.Instance == null) return;

        // --- 1. MOVER TODO EL TREN ---
        // Movemos el objeto PADRE (y por tanto todos los hijos se mueven a la vez)
        // Esto garantiza que JAMÁS se separen.
        float velocidad = BGMOVEMENT.Instance.velocidadActual * factorVelocidad;
        
        // Movemos las piezas manualmente una a una para tener control total
        foreach(Transform pieza in piezas)
        {
            pieza.Translate(Vector3.left * velocidad * Time.deltaTime);
        }

        // --- 2. RECICLAR EL PRIMERO ---
        // Miramos siempre al primer objeto de la lista (el que está más a la izquierda)
        Transform primeraPieza = piezas[0];

        if (primeraPieza.position.x < limiteIzquierda)
        {
            MandarAlFinal(primeraPieza);
        }
    }

    void MandarAlFinal(Transform piezaAMover)
    {
        // 1. Identificamos cuál es la ULTIMA pieza de la fila ahora mismo
        Transform ultimaPieza = piezas[piezas.Count - 1];

        // 2. Colocamos la pieza vieja JUSTO detrás de la última
        // Usamos la posición de la última + el ancho exacto.
        Vector3 nuevaPos = ultimaPieza.position;
        nuevaPos.x += anchoPieza; 
        
        // (Opcional: Restar 0.01f si ves una línea negra vertical entre sprites)
        // nuevaPos.x -= 0.01f;

        piezaAMover.position = nuevaPos;

        // 3. Actualizamos la lista: La primera pasa a ser la última
        piezas.RemoveAt(0);
        piezas.Add(piezaAMover);
    }

    // AYUDA VISUAL EN EL EDITOR
    private void OnDrawGizmos()
    {
        // Dibuja la línea donde mueren los objetos
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(limiteIzquierda, -10, 0), new Vector3(limiteIzquierda, 10, 0));
    }
}
