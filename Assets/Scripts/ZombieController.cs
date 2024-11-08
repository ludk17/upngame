using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float velocidad = 2f;         // Velocidad de movimiento
    private Rigidbody2D rb;
    private bool moviendoDerecha = false; // Por defecto empieza movi�ndose a la izquierda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Mover en la direcci�n actual
        float direccion = moviendoDerecha ? 1 : -1;
        rb.velocity = new Vector2(direccion * velocidad, rb.velocity.y);

        // Si el sprite mira a la izquierda por defecto, usar esta l�nea en su lugar:
        transform.localScale = new Vector3(-direccion, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {        
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Calculo para la normal de la colisi�n
            Vector2 normal = collision.contacts[0].normal;

            // Si la colisi�n es m�s horizontal que vertical
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                CambiarDireccion();
            }
        }
    }

    void CambiarDireccion()
    {
        moviendoDerecha = !moviendoDerecha;
    }
}