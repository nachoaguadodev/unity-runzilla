using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Godzilla : MonoBehaviour
{
    public float fuerzaSalto = 12f;
    public LayerMask capaSuelo;

    public float longitudExtra = 0.05f; 
    public float separacionPies = 0.4f;

    public float multiplicadorCaida = 2.5f;
    public float multiplicadorSaltoCorto = 2f;
    public AudioClip sonidoSalto;
    public AudioClip sonidoAterrizaje;
    public AudioClip sonidoChoque;
    [Range(0, 1)] public float volumen = 0.5f;
    public float velocidadPasos = 20f;
    public float anguloInclinacion = 5f;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private bool enSuelo = false;
    private float tiempoEnAire = 0f;
    private AudioSource miAudioSource;
    private bool estaMuerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 

        miAudioSource = GetComponent<AudioSource>();
        if (miAudioSource == null) miAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (estaMuerto) return;

        VerificarSuelo();

        //ATERRIZAJE
        if (enSuelo)
        {
            if (tiempoEnAire > 0.15f) 
            {
                ReproducirSonido(sonidoAterrizaje);
                transform.rotation = Quaternion.identity; 
            }
            tiempoEnAire = 0f;
        }
        else
        {
            tiempoEnAire += Time.deltaTime;
        }

        // SALTO
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && enSuelo) 
        {
            Saltar();
        }

        AjustarGravedad();
        AnimarSprite();
    }

    //DETECCIÓN DE SUELO CON DOBLE RAYO
    void VerificarSuelo()
    {
        Vector2 centroPies = new Vector2(col.bounds.center.x, col.bounds.min.y);
        Vector2 origenIzq = centroPies + new Vector2(-separacionPies, 0.1f);
        Vector2 origenDer = centroPies + new Vector2(separacionPies, 0.1f);

        RaycastHit2D hitIzq = Physics2D.Raycast(origenIzq, Vector2.down, longitudExtra + 0.1f, capaSuelo);
        RaycastHit2D hitDer = Physics2D.Raycast(origenDer, Vector2.down, longitudExtra + 0.1f, capaSuelo);

        enSuelo = (hitIzq.collider != null || hitDer.collider != null);

        Debug.DrawRay(origenIzq, Vector2.down * (longitudExtra + 0.1f), hitIzq.collider != null ? Color.green : Color.red);
        Debug.DrawRay(origenDer, Vector2.down * (longitudExtra + 0.1f), hitDer.collider != null ? Color.green : Color.red);
    }

    void AnimarSprite()
    {
        if (enSuelo && !estaMuerto)
        {
            float rotacionZ = Mathf.Sin(Time.time * velocidadPasos) * anguloInclinacion;
            transform.rotation = Quaternion.Euler(0, 0, rotacionZ);
        }
        else
        {
            transform.rotation = Quaternion.identity; 
        }
    }

    void Saltar()
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        ReproducirSonido(sonidoSalto);
        tiempoEnAire = 0.1f;
        
    }

    void AjustarGravedad()
    {
        // 1. Guardamos en una variable si el jugador está manteniendo CUALQUIERA de los controles
        bool manteniendoSalto = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.touchCount > 0;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (multiplicadorCaida - 1) * Time.deltaTime;
        }
        // 2. Ahora comprobamos nuestra nueva variable en lugar de solo el Space
        else if (rb.velocity.y > 0 && !manteniendoSalto)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (multiplicadorSaltoCorto - 1) * Time.deltaTime;
        }
    }

    void ReproducirSonido(AudioClip clip)
    {
        if (clip != null && miAudioSource != null) miAudioSource.PlayOneShot(clip, volumen);
    }

    //COLISIONES Y MUERTE

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (estaMuerto) return;
        if (other.CompareTag("Obstaculo")) StartCoroutine(SecuenciaMuerte());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (estaMuerto) return;
        // DETECCIÓN DE FANTASMAS
        if (collision.gameObject.CompareTag("Fantasma"))
        {
            StartCoroutine(SecuenciaMuerte());
        }
    }

    IEnumerator SecuenciaMuerte()
    {
        estaMuerto = true;
        ReproducirSonido(sonidoChoque);
        if (BGMOVEMENT.Instance != null)
        {
            BGMOVEMENT.Instance.StopGame(); 
        }

        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque(5f, ForceMode2D.Impulse);
        rb.velocity = new Vector2(-5f, 5f);
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(1f);
        
        //MOSTRAMOS UI DE GAME OVER 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ActivarGameOver();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}