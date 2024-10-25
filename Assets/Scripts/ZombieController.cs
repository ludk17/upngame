using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float direccionX = -1;  // Iniciar caminando hacia la izquierda

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Obtener el SpriteRenderer del objeto
    }

    // Update is called once per frame
    void Update()
    {
        // Aplicar movimiento en el eje X según la dirección
        rb.velocity = new Vector2(direccionX * 3, rb.velocity.y);  // velocidad 3 en la dirección que esté
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un objeto que tiene el tag "Pared"
        if (collision.gameObject.CompareTag("pared"))
        {
            // Cambiar la dirección en el eje X
            direccionX *= -1;  // Invertir la dirección

            // Flip del sprite en el eje X (invertir la dirección visual)
            spriteRenderer.flipX = !spriteRenderer.flipX;

            Debug.Log("El zombie ha chocado con la pared y cambió de dirección.");
        }
    }
}
