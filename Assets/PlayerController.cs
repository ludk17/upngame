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
    public AudioClip audioClipSalto;

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;

    private bool gravedadEstaActivada = true;

    private GameObject gameManager;
    private GameObject playerMessage;
    // Start is called before the first frame update
    void Start()
    {
        // acceder a rigidbody
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");
        
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
            audioSource.PlayOneShot(audioClipSalto);
            animator.SetInteger("Estado", ANIMATION_JUMP); // no esta funcionando
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

}
