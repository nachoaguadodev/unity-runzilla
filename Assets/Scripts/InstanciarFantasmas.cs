using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarFantasmas : MonoBehaviour
{
    public GameObject fantasma0, fantasma1, fantasma2, fantasma3;

      private Camera camara;
      



    // Start is called before the first frame update
    void Start()
    {
        
        camara = Camera.main;
        generarFantasmas();
        StartCoroutine(GenerarCada10Segundos());

    }
     IEnumerator GenerarCada10Segundos() //Corrutina que "Pausa la ejecucion y la reanuda en el tiempo especificado
    {
        while (true)
        {
            generarFantasmas();
            yield return new WaitForSeconds(10f);
        }
    }



    public void generarFantasmas()
    {
        int QueFantasma = Random.Range(0, 4); // 0,1,2 o 3
        GameObject ghost = null;

        switch (QueFantasma)
        {
            case 0: ghost = fantasma0; break;
            case 1: ghost = fantasma1; break;
            case 2: ghost = fantasma2; break;
            case 3: ghost = fantasma3; break;
        }        

        // ➤ Calculamos el borde derecho de la cámara en coordenadas del mundo
        float bordeDerecho = camara.ViewportToWorldPoint(new Vector3(1, 0, camara.nearClipPlane)).x;

        // ➤ Altura aleatoria dentro de la cámara
        float alturaAleatoria = Random.Range(
            camara.ViewportToWorldPoint(new Vector3(0, 0, camara.nearClipPlane)).y,
            camara.ViewportToWorldPoint(new Vector3(0, 1, camara.nearClipPlane)).y
        );

        // ➤ Instanciamos el fantasma justo fuera del borde derecho
        GameObject a = Instantiate(ghost);
        a.transform.position = new Vector3(bordeDerecho + 1f, alturaAleatoria, 0f);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

}
