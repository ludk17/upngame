using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarZombies : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public float generationInterval = 10f;
    SpriteRenderer sr;
    float tiempo=0f;
    private float moveSpeed = 2f;
    private float moveRange = 5f;

    Vector2 StartPosition;

    GameObject Zombie;
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        generador();

        float oscilation = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        transform.position = StartPosition + new Vector2(oscilation, 0);
    }

    void generador()
    {
        tiempo += Time.deltaTime;
        if (tiempo>=1)
        {
            Zombie = Instantiate(ZombiePrefab, transform.position, Quaternion.identity);
            tiempo = 0;
        }
    }
}
