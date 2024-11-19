using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public AudioClip jumpsound;    

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

    // Flags para los botones
    private bool moveLeft = false;
    private bool moveRight = false;
    // Start is called before the first frame update

    // variables para interactuar con los botones
    private float velocityX = 0f;
    private bool saltar = false;


    void Start()
    {
        // acceder a rigidbody
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");
        audioSource = gameManager.AddComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update() {
        // // modificar el rigidibody

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
        
        if (velocityX != 0) {
            animator.SetInteger("Estado", ANIMATION_RUN);
        } else {
            animator.SetInteger("Estado", ANIMATION_IDLE);
        }
        
        animator.SetInteger("Estado", ANIMATION_IDLE);

        // Movimiento hacia la derecha
        if (moveRight)
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            sr.flipX = false;
            animator.SetInteger("Estado", ANIMATION_RUN);
        }

        // Movimiento hacia la izquierda
        if (moveLeft)
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            sr.flipX = true;
            animator.SetInteger("Estado", ANIMATION_RUN);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(10, rb.velocity.y);            
            sr.flipX = false;
        } else if (velocityX < 0) {
            sr.flipX = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) && !gravedadEstaActivada) {
                rb.velocity = new Vector2(rb.velocity.x, 10);            
        }
       
        // if (gravedadEstaActivada) {
        //     rb.velocity = new Vector2(0, rb.velocity.y);
        // } else {
        //     rb.velocity = new Vector2(0, 0);
        // }
        
        // animator.SetInteger("Estado", ANIMATION_IDLE);

        // // GetKeyUp: se ejecuta cuando se suelta la tecla
        // // GetKeyDown: se ejecuta cuando se presiona la tecla
        // // GetKey: se ejecuta mientras se mantiene presionada la tecla
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     rb.velocity = new Vector2(10, rb.velocity.y);            
        //     sr.flipX = false;
        // }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.PlayOneShot(jumpsound);
            rb.velocity = new Vector2(rb.velocity.x, 10);            
            animator.SetInteger("Estado", ANIMATION_JUMP); // no esta funcionando            
        }        
    }

    public void MoveLeft(bool isPressed)
    {
        moveLeft = isPressed;
    }

    public void MoveRight(bool isPressed)
    {
        moveRight = isPressed;
    }

    public void Jump()
    {
        if (gravedadEstaActivada && rb.velocity.y == 0) // Salta solo si est� en el suelo
        {
            audioSource.PlayOneShot(jumpsound);
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Debug.Log("Colision con Enemigo");
            gameManager.GetComponent<GameManagerController>().RemoveLife();
            playerMessage.GetComponent<TextMeshProUGUI>().text = "Ouch!";
            playerMessage.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Invoke("HideMessage", 1);
        }

        if (collision.gameObject.name == "Coin") {
            // hacer 2
            Debug.Log("Colision con Coin");
        }

        if (collision.gameObject.name == "Finish") {
            // hacer 3
            Debug.Log("Colision con Finish");
        }

        if (collision.gameObject.tag == "Recollectable") {
            var gameManagerC = gameManager.GetComponent<GameManagerController>();
            gameManagerC.AddKunai(3);
            Destroy(collision.gameObject);
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

    private void HideMessage() {
        playerMessage.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void Jump() {
        saltar = true;
    }

    public void WalkRight() {
        velocityX = 10;
    }

    public void WalkLeft() {
        velocityX = -10;
    }
    
    public void WalkStop() {
        velocityX = 0;
    }

}
