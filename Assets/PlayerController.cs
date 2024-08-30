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

    // Start is called before the first frame update
    void Start()
    {
        // acceder a rigidbody
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // // modificar el rigidibody
        
        // GetKeyUp: se ejecuta cuando se suelta la tecla
        // GetKeyDown: se ejecuta cuando se presiona la tecla
        // GetKey: se ejecuta mientras se mantiene presionada la tecla
        
        rb.velocity = new Vector2(0, rb.velocity.y);
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

        if (rb.velocity.x != 0) {
            animator.SetInteger("Estado", ANIMATION_RUN);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10);
            animator.SetInteger("Estado", ANIMATION_JUMP);
        }
        
    }
}
