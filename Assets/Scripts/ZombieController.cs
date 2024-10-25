using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Rigidbody2D rb;
    private float moveDuration = 8f; 
    private float moveSpeed = 4f;
    private float timer = 0f; 
    private bool movingRight; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movingRight = Random.value > 0.5f; 
       
        transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
    }

    void Update()
    {
        
        timer += Time.deltaTime;

        if (timer >= moveDuration)
        {
            movingRight = !movingRight; 
            timer = 0f;
            transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
        }

        float direction = movingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }
}
