using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask Pared;
    [SerializeField] private GameObject zombie;
    private float tiempoZom;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-4 * transform.right.x, rb.velocity.y);
        RaycastHit2D infSuelo = Physics2D.Raycast(transform.position, -transform.right, distancia, Pared);
        tiempoZom += Time.deltaTime;

        if (tiempoZom >= 3)
        {
            tiempoZom = 0;
            GenerarZom();
        }

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

    private void GenerarZom()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(zombie, pos, Quaternion.identity);
    }
}
