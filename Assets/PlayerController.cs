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
    private const int ANIMATION_CORTAR = 3;
    private const int ANIMATION_DISPARAR = 4;

    private bool gravedadEstaActivada = true;

    private GameManagerController gameManager;
    private TextMeshProUGUI playerMessage;

    // Input control
    private float velocityX = 0f;
    private bool saltar = false;
    private bool cortar = false;
    private bool disparar = false;

    // Start is called before the first frame update
    void Start()
    {
        // Cache components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerController>();
        playerMessage = GameObject.Find("PlayerMessage").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleActions();
    }

    // Handle player movement input
    private void HandleMovement()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);

        if (velocityX > 0)
            sr.flipX = false;
        else if (velocityX < 0)
            sr.flipX = true;
    }

    // Handle player animations based on the movement
    private void HandleAnimation()
    {
        if (velocityX != 0)
        {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }
        else
        {
            animator.SetInteger("Estado", ANIMATION_IDLE);
        }

        if (saltar)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP);
            saltar = false;
        }

        if (cortar)
        {
            animator.SetInteger("Estado", ANIMATION_CORTAR);
            cortar = false;
        }

        if (disparar) {
            animator.SetInteger("Estado", ANIMATION_DISPARAR);
            disparar = false;
        }
    }

    // Handle player actions like jump and sword attack
    private void HandleActions()
    {
        if (saltar)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP);
            saltar = false;
        }

        if (cortar)
        {
            animator.SetInteger("Estado", ANIMATION_CORTAR);
            cortar = false;
        }

        if (disparar) {
            animator.SetInteger("Estado", ANIMATION_DISPARAR);
            disparar = false;
        }
    }

    // Collision detection and handling
    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                HandleEnemyCollision();
                break;
            case "Coin":
                HandleCoinCollision();
                break;
            case "Finish":
                HandleFinishCollision();
                break;
            case "Recollectable":
                HandleRecollectableCollision(collision);
                break;
        }
    }

    private void HandleEnemyCollision()
    {
        Debug.Log("Colision con Enemigo");
        gameManager.RemoveLife();
        ShowPlayerMessage("Ouch!");
    }

    private void HandleCoinCollision()
    {
        Debug.Log("Colision con Coin");
        // Add coin collection logic here
    }

    private void HandleFinishCollision()
    {
        Debug.Log("Colision con Finish");
        // Add finish line logic here
    }

    private void HandleRecollectableCollision(Collision2D collision)
    {
        gameManager.AddKunai(3);
        Destroy(collision.gameObject);
    }

    // Trigger collision handling
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

    // Display player message
    private void ShowPlayerMessage(string message)
    {
        playerMessage.text = message;
        playerMessage.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Invoke("HideMessage", 1);
    }

    private void HideMessage()
    {
        playerMessage.text = "";
    }

    // Action triggers (called from external input)
    public void Jump()
    {
        saltar = true;
    }

    public void Espadazo()
    {
        cortar = true;
        Debug.Log("Espadazo, fuaa");
    }

    public void Disparar()
    {
        disparar = true;
    }

    public void WalkRight()
    {
        velocityX = 10;
    }

    public void WalkLeft()
    {
        velocityX = -10;
    }

    public void WalkStop()
    {
        velocityX = 0;
    }
}
