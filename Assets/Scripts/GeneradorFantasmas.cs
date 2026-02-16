using UnityEngine;

public class GeneradorFantasmas : MonoBehaviour
{
    public GameObject[] fantasmas;
    public Transform puntoSuelo; 
    public Transform puntoAire;
    public float radioSeguridad = 2.5f; 
    public float tiempoMinimo = 2f;
    public float tiempoMaximo = 4.5f;
    private float temporizador;
    private GameObject contenedor;

    void Start()
    {
        temporizador = Random.Range(tiempoMinimo, tiempoMaximo);
        contenedor = new GameObject("--- Fantasmas Activos ---");
    }

    void Update()
    {
        if (BGMOVEMENT.Instance == null || BGMOVEMENT.Instance.velocidadActual <= 0) return;
        if (Time.timeScale == 0) return;

        temporizador -= Time.deltaTime;

        if (temporizador <= 0)
        {
            IntentarGenerarFantasma(); // Hemos cambiado el nombre de la función
            temporizador = Random.Range(tiempoMinimo, tiempoMaximo);
        }
    }

    void IntentarGenerarFantasma()
    {
        if (fantasmas.Length == 0 || puntoSuelo == null || puntoAire == null) return;

        bool spawnArriba = (Random.value > 0.5f);
        Vector3 posicionFinal = spawnArriba ? puntoAire.position : puntoSuelo.position;

        //DETECCIÓN DE OBSTÁCULOS
        
        Collider2D[] colisiones = Physics2D.OverlapCircleAll(posicionFinal, radioSeguridad);

        foreach (Collider2D col in colisiones)
        {
            // Si tocamos algo que sea un Obstaculo
            if (col.CompareTag("Obstaculo"))
            {
                return; // No se crea el fantasma.
            }
        }

        //Elegir fantasma y crear.
        int indiceFantasma = Random.Range(0, fantasmas.Length);
        
        GameObject nuevoFantasma = Instantiate(fantasmas[indiceFantasma], posicionFinal, Quaternion.identity);
        
        if (contenedor != null) nuevoFantasma.transform.parent = contenedor.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 

        if (puntoSuelo != null)
        {
            Gizmos.DrawWireSphere(puntoSuelo.position, radioSeguridad);
        }

        if (puntoAire != null)
        {
            Gizmos.DrawWireSphere(puntoAire.position, radioSeguridad);
        }
    }
}