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

    private GameObject gameManager;
    private GameObject playerMessage;

    // Variables
    private float velocityx = 0f;
    private bool saltar = false;

    void Start()
    {
        // Acceder a Rigidbody
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        playerMessage = GameObject.Find("PlayerMessage");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityx, rb.velocity.y);

        if (velocityx != 0)
        {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }
        else
        {
            animator.SetInteger("Estado", ANIMATION_IDLE);
        }

        if (velocityx > 0)
        {
            sr.flipX = false;
        }
        else if (velocityx < 0) // Corregido: falta la condición para el movimiento a la izquierda
        {
            sr.flipX = true;
        }

        if (saltar)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10); // Cambiado a Vector2
            animator.SetInteger("Estado", ANIMATION_JUMP);
            saltar = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Lógica para colisiones
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        // Lógica para permanecer en trigger
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // Lógica para salir del trigger
    }

    private void HideMessage()
    {
        // Lógica para ocultar mensaje
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 10); // Cambiado a Vector2
        animator.SetInteger("Estado", ANIMATION_JUMP);
        saltar = true; // Activar el salto
    }

    public void WalkRight()
    {
        velocityx = 10;
    }

    public void WalkLeft()
    {
        velocityx = -10; // Corregido para caminar a la izquierda
    }

    public void WalkStop()
    {
        velocityx = 0;
    }
}

