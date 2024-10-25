using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask Pared;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-4* transform.right.x, rb.velocity.y);
        RaycastHit2D infSuelo = Physics2D.Raycast(transform.position, -transform.right, distancia, Pared);
 

        if (infSuelo)
        {
            Girar();
        }
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * distancia);
    }


}
