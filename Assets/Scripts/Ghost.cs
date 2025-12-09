using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    private Camera camara;
    private float limiteIzdo;
    private float velocidad;
    private Vector3 direccion;




    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main;

        // Detectar tipo de fantasma según su nombre
        if (gameObject.name.Contains("fantasma0"))
        {
            velocidad = 5f;
            direccion = Vector3.left;
        }
        else if (gameObject.name.Contains("fantasma1"))
        {
            velocidad = 8f;
            direccion = Vector3.left; 
        }
        else if (gameObject.name.Contains("fantasma2"))
        {
            velocidad = 3f;
            direccion = Vector3.left; 
        }
        else if (gameObject.name.Contains("fantasma3"))
        {
            velocidad = 10f;
            direccion = Vector3.left; 
        }


        //Calcula al límite izdo de la cámara en coordenadas del mundo
        limiteIzdo = camara.ViewportToWorldPoint(new Vector3(0,0,0)).x;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Alcualiza el límite izdo
        limiteIzdo = camara.ViewportToWorldPoint(new Vector3(0,0,0)).x;

        this.transform.Translate(direccion * velocidad * Time.deltaTime);
        
        //Para que se destruya a sí mismo al salir de pantalla
        if (this.transform.position.x < limiteIzdo -1f){
            Destroy(this.gameObject);
        }
    }

    /* private void OnTriggerEnter (Collider other){
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }*/

    private void OnDestroy(){
        Debug.Log("Nooooo");
    }

}
