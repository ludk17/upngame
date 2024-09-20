using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;

    private bool gravedadEstaActivada = true;

    // Start is called before the first frame update
    void Start()
    {
        // acceder a rigidbody
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update() {
        // // modificar el rigidibody
        
        // GetKeyUp: se ejecuta cuando se suelta la tecla
        // GetKeyDown: se ejecuta cuando se presiona la tecla
        // GetKey: se ejecuta mientras se mantiene presionada la tecla
        if (gravedadEstaActivada) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(0, 0);
        }
        
        animator.SetInteger("Estado", ANIMATION_IDLE);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(10, rb.velocity.y);            
            sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            sr.flipX = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) && !gravedadEstaActivada) {
                rb.velocity = new Vector2(rb.velocity.x, 10);
        }
        
        if (Input.GetKey(KeyCode.DownArrow) && !gravedadEstaActivada) {
            rb.velocity = new Vector2(rb.velocity.x, -10);
        }

        if (rb.velocity.x != 0) {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP); // no esta funcionando
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Debug.Log("Colision con Enemigo");
            // hacer 1
        }

        if (collision.gameObject.name == "Coin") {
            // hacer 2
            Debug.Log("Colision con Coin");
        }

        if (collision.gameObject.name == "Finish") {
            // hacer 3
            Debug.Log("Colision con Finish");
        }
        
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.name == "Pared") {
            rb.gravityScale = 0;
            gravedadEstaActivada = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.name == "Pared") {
            rb.gravityScale = 1;
            gravedadEstaActivada = true;
        }
    }

}
