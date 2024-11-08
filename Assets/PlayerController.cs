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
    private const int ANIMATION_ATTACK = 4; 

    private bool gravedadEstaActivada = true;

    private GameObject gameManager;
    private GameObject playerMessage;
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
        
    }

    // Update is called once per frame
    void Update() {
    // Si está atacando, no se puede cambiar de animación (esto es opcional)
    if (animator.GetInteger("Estado") == ANIMATION_ATTACK) {
        return; // No hacer nada más si está atacando
    }
    
    rb.velocity = new Vector2(velocityX, rb.velocity.y);
    
    if (velocityX != 0) {
        animator.SetInteger("Estado", ANIMATION_RUN);
    } else {
        animator.SetInteger("Estado", ANIMATION_IDLE);
    }
    
    if (saltar) {
        rb.velocity = new Vector2(rb.velocity.x, 10);
        animator.SetInteger("Estado", ANIMATION_JUMP);
        saltar = false;
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
   public void Attack() {
    animator.SetInteger("Estado", ANIMATION_ATTACK);
    // Después de 0.5 segundos (ajustar según la duración de tu animación), volver a Idle o Run
    Invoke("ReturnToIdle", 0.5f); // Esto asume que la animación de ataque dura 0.5 segundos
    }

    private void ReturnToIdle() {
    // Si el personaje no se está moviendo, regresa al estado Idle
    if (velocityX == 0) {
        animator.SetInteger("Estado", ANIMATION_IDLE);
    } else {
        animator.SetInteger("Estado", ANIMATION_RUN);
    }
    }
}
