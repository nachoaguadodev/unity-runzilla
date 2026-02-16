using UnityEngine;

public class ObjetosTerrestres : MonoBehaviour
{
    public GameObject[] obstaculos;

    // Distancia mínima y máxima entre el final de un obstáculo y el principio del siguiente
    public float distanciaMinima = 3f; 
    public float distanciaMaxima = 7f;

    private float tiempoParaSiguienteSpawn;

    void Update()
    {
        if (BGMOVEMENT.Instance == null) return;

        tiempoParaSiguienteSpawn -= Time.deltaTime;

        if (tiempoParaSiguienteSpawn <= 0f)
        {
            GenerarObstaculo();
        }
    }

    void GenerarObstaculo()
    {
        // Elegimos obstáculo aleatorio
        int indice = Random.Range(0, obstaculos.Length);
        GameObject prefabElegido = obstaculos[indice];

        // Instanciamos en la posición de este Generador
        GameObject nuevoObstaculo = Instantiate(prefabElegido, transform.position, Quaternion.identity);

        // Evitar Superposición
        // Obtenemos el script del obstáculo para saber su ancho
        MovimientoObstaculo scriptMov = nuevoObstaculo.GetComponent<MovimientoObstaculo>();
        float anchoObstaculo = 1f; // Valor por defecto por seguridad

        if (scriptMov != null)
        {
            // Forzamos el Start manual si es necesario, o esperamos al frame siguiente,
            // pero para simplificar usamos el collider del prefab original si es posible
            BoxCollider2D col = prefabElegido.GetComponent<BoxCollider2D>();
            if(col != null) anchoObstaculo = col.size.x * prefabElegido.transform.localScale.x;
        }

        float velocidadActual = BGMOVEMENT.Instance.velocidadActual;
        
        // Si la velocidad es 0, evitamos división por cero
        if (velocidadActual <= 0) velocidadActual = 1f;

        float distanciaExtra = Random.Range(distanciaMinima, distanciaMaxima);
        
        // El tiempo de espera eslo que tarda en pasar el ancho del objeto + la distancia extra
        tiempoParaSiguienteSpawn = (anchoObstaculo + distanciaExtra) / velocidadActual;
    }
    
    // Dibujo para saber dónde está el spawner
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
        Gizmos.DrawIcon(transform.position, "sv_icon_dot10_pix16_gizmo", true);
    }
}