using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    float direccionx=-1;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        rb.velocity = new Vector2(direccionx*3, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            direccionx*=-1;
            sr.flipX = !sr.flipX;
        }
    }
}
