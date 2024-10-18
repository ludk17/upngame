using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;

    private bool gravedadEstaActivada = true;

    private GameObject gameManager;
    private GameObject playerMessage;

    // Sonido
    [SerializeField] private AudioClip saltoSonido; // Sonido del salto
    [SerializeField] private GameObject pisoChoque; // Objeto de colisión con el suelo

    void Start()
    {
        // Acceder a componentes
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");

        // Cargar el objeto pisoChoque
        pisoChoque = GameObject.Find("PersonajePiso");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Modificar el Rigidbody
        if (gravedadEstaActivada)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        animator.SetInteger("Estado", ANIMATION_IDLE);

        // Movimiento a la derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            sr.flipX = false;
        }

        // Movimiento a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            sr.flipX = true;
        }

        // Movimiento hacia arriba (salto)
        if (Input.GetKey(KeyCode.UpArrow) && !gravedadEstaActivada)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }

        // Movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow) && !gravedadEstaActivada)
        {
            rb.velocity = new Vector2(rb.velocity.x, -10);
        }

        // Cambio de estado a correr
        if (rb.velocity.x != 0)
        {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }

        // Salto con espacio
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP); // Cambiar a animación de salto
            audioSource.PlayOneShot(saltoSonido); // Reproducir sonido de salto
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisión con enemigo
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Colisión con Enemigo");
            gameManager.GetComponent<GameManagerController>().RemoveLife();
            playerMessage.GetComponent<TextMeshProUGUI>().text = "Ouch!";
            playerMessage.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Invoke("HideMessage", 1);
        }

        // Colisión con Coin
        if (collision.gameObject.name == "Coin")
        {
            Debug.Log("Colisión con Coin");
        }

        // Colisión con Finish
        if (collision.gameObject.name == "Finish")
        {
            Debug.Log("Colisión con Finish");
        }

        // Colisión con Recollectable
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
