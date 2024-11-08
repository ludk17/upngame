using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public AudioClip saltoSound;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    private PlayerAttackController attackController;

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;
    private const int ANIMATION_ATTACK = 3;
    private bool isAttacking = false;

    private bool gravedadEstaActivada = true;

    private GameObject gameManager;
    private GameObject playerMessage;

    private float velocityX = 0f;
    private bool saltar = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");
        attackController = GetComponent<PlayerAttackController>();

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        if (saltoSound == null)
        {
            Debug.LogError("El clip de sonido 'saltoSound' no está asignado en el inspector.");
        }
    }

    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);

        if (velocityX != 0)
        {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }
        else
        {
            animator.SetInteger("Estado", ANIMATION_IDLE);
        }

        if (velocityX > 0)
        {
            sr.flipX = false;
        }
        else if (velocityX < 0)
        {
            sr.flipX = true;
        }

        if (saltar)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP);
            saltar = false;
        }

        if (isAttacking)
        {
            animator.SetInteger("Estado", ANIMATION_ATTACK);
            attackController.Attack(); 
            isAttacking = false;  
        }
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
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
            Debug.Log("Colisión con Coin");
        }

        if (collision.gameObject.name == "Finish")
        {
            Debug.Log("Colisión con Finish");
        }

        if (collision.gameObject.tag == "Recollectable")
        {
            gameManager.GetComponent<GameManagerController>().AddKunai(3);
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

    public void Jump()
    {
        saltar = true;
    }
    public void Attack()
    {
        isAttacking = true;
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
