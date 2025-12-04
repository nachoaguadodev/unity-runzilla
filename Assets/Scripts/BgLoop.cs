using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLoop : MonoBehaviour
{
    public Transform[] backgrounds;    // Tus 2 fondos
    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f; // 0 = se queda fijo, 1 = se mueve como la cámara

    private float spriteWidth;
    private Camera cam;
    private float lastCamX;

    void Start()
    {
        cam = Camera.main;
        lastCamX = cam.transform.position.x;

        // Usamos el ancho del primer fondo (los dos deberían ser iguales)
        SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }

    void LateUpdate()
    {
        float camX = cam.transform.position.x;
        float deltaX = camX - lastCamX;       // Cuánto se ha movido la cámara desde el frame anterior
        lastCamX = camX;

        // Parallax: mover los fondos un poco menos que la cámara
        foreach (Transform bg in backgrounds)
        {
            bg.position += Vector3.right * deltaX * (parallaxFactor - 1f);
            // Explicación:
            // si parallaxFactor = 0.5 → fondos se mueven la mitad que la cámara
            // si = 0   → casi "infinitamente lejos"
            // si = 1   → se pegan a la cámara (sin parallax)
        }

        // Reciclado: si un fondo se ha quedado muy atrás, lo mandamos delante
        foreach (Transform bg in backgrounds)
        {
            // Distancia de la cámara al fondo
            if (cam.transform.position.x - bg.position.x > spriteWidth)
            {
                // Buscamos el fondo más a la derecha
                Transform rightMost = backgrounds[0];
                if (backgrounds[1].position.x > rightMost.position.x)
                    rightMost = backgrounds[1];

                // Colocamos este fondo justo a la derecha del más derecho
                bg.position = new Vector3(
                    rightMost.position.x + spriteWidth,
                    bg.position.y,
                    bg.position.z
                );
            }
        }
    }
}
