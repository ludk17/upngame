using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un objeto que tiene el tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Obtener el Rigidbody2D del enemigo
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Obtener el SpriteRenderer del enemigo
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log("El enemigo ha chocado con la pared y ha cambiado de dirección.");

            // Verificar si los componentes fueron encontrados antes de proceder
            if (rb != null && spriteRenderer != null)
            {
                // Invertir la dirección en el eje X
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);

                // Flip del sprite en el eje X (invertir la dirección visual)
                spriteRenderer.flipX = !spriteRenderer.flipX;

                // Cambiar la posición del enemigo si es necesario (esto es opcional)
                // Ejemplo: mover al enemigo ligeramente en el eje X
                collision.transform.position = new Vector2(collision.transform.position.x + 1f, collision.transform.position.y);

                Debug.Log("El enemigo ha chocado con la pared y ha cambiado de dirección.");
            }
            else
            {
                Debug.LogError("El enemigo no tiene Rigidbody2D o SpriteRenderer.");
            }
        }
    }
}
