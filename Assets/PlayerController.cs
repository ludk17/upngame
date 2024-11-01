using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;

    private bool gravedadEstaActivada = true;
    private bool estaEnSuelo = true;

    private GameObject gameManager;
    private GameObject playerMessage;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");
    }
    // Función para moverse a la derecha
    public void MoverDerecha()
    {
        rb.velocity = new Vector2(10, rb.velocity.y);
        sr.flipX = false;
        animator.SetInteger("Estado", ANIMATION_RUN);
    }
    // Función para moverse a la izquierda
    public void MoverIzquierda()
    {
        rb.velocity = new Vector2(-10, rb.velocity.y);
        sr.flipX = true;
        animator.SetInteger("Estado", ANIMATION_RUN);
    }

    // Función para moverse hacia arriba (solo si la gravedad está desactivada)
    public void MoverArriba()
    {
        if (!gravedadEstaActivada)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }
    }

    // Función para moverse hacia abajo (solo si la gravedad está desactivada)
    public void MoverAbajo()
    {
        if (!gravedadEstaActivada)
        {
            rb.velocity = new Vector2(rb.velocity.x, -10);
        }
    }

  
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP);
            estaEnSuelo = false; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {
            estaEnSuelo = true; 
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Colisión con Enemigo");
            gameManager.GetComponent<GameManagerController>().RemoveLife();
            playerMessage.GetComponent<TextMeshProUGUI>().text = "Ouch!";
            playerMessage.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Invoke("HideMessage", 1);
        }

        if (collision.gameObject.name == "Coin")
        {
            Debug.Log("Colisión con Moneda");
        }

        if (collision.gameObject.name == "Finish")
        {
            Debug.Log("Colisión con Final");
        }

        if (collision.gameObject.tag == "Recollectable")
        {
            var gameManagerC = gameManager.GetComponent<GameManagerController>();
            gameManagerC.AddKunai(3);
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Pared")
        {
            rb.gravityScale = 0;
            gravedadEstaActivada = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Pared")
        {
            rb.gravityScale = 1;
            gravedadEstaActivada = true;
        }
    }

    private void HideMessage()
    {
        playerMessage.GetComponent<TextMeshProUGUI>().text = "";
    }
}