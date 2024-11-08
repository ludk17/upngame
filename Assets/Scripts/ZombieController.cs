using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 2f;         // Velocidad de movimiento
    private Rigidbody2D rb;
    private bool moviendoDerecha = false; // Por defecto empieza moviéndose a la izquierda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Mover en la dirección actual
        float direccion = moviendoDerecha ? 1 : -1;
        rb.velocity = new Vector2(direccion * velocidad, rb.velocity.y);

        // Si el sprite mira a la izquierda por defecto, usar esta línea en su lugar:
        transform.localScale = new Vector3(-direccion, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {        
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Calculo para la normal de la colisión
            Vector2 normal = collision.contacts[0].normal;

            // Si la colisión es más horizontal que vertical
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