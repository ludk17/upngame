using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip jumpSound;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;

    private bool gravedadEstaActivada = true;

    private GameObject gameManager;
    private GameObject playerMessage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (gravedadEstaActivada) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(0, 0);
        }
        
        animator.SetInteger("Estado", ANIMATION_IDLE);

        if (rb.velocity.x != 0) {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }
    }

    // Método público para moverse a la izquierda
    public void MoveLeft()
    {
        rb.velocity = new Vector2(-10, rb.velocity.y);
        sr.flipX = true;
        animator.SetInteger("Estado", ANIMATION_RUN);
    }

    // Método público para moverse a la derecha
    public void MoveRight()
    {
        rb.velocity = new Vector2(10, rb.velocity.y);
        sr.flipX = false;
        animator.SetInteger("Estado", ANIMATION_RUN);
    }

    // Método público para saltar
    public void Jump()
    {
        if (!gravedadEstaActivada) {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            audioSource.PlayOneShot(jumpSound);
            animator.SetInteger("Estado", ANIMATION_JUMP);
        }
    }

    // Método público para atacar
    public void Attack()
    {
        GetComponent<PlayerAttackController>().Attack();
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
            Debug.Log("Colision con Coin");
        }

        if (collision.gameObject.name == "Finish") {
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
