using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaSpawn : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float Intervalo = 2f;
    public float Velocidad = 2f;
    public float LimiteIzquierda = -2.5f;
    public float LimiteDerecha = 2.5f;
    public float Altura = 12f;
    private bool Control = true;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, Altura, transform.position.z);
        StartCoroutine(SpawnZombies());
    }

    void Update()
    {
        MovimientoCaja();
    }

    private IEnumerator SpawnZombies()
    {
        while (true)
        {
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Intervalo);
        }
    }

    private void MovimientoCaja()
    {
        float xPosition = transform.position.x + (Control ? Velocidad * Time.deltaTime : -Velocidad * Time.deltaTime);
        transform.position = new Vector3(xPosition, Altura, transform.position.z);
        if (transform.position.x >= LimiteDerecha) Control = false;
        if (transform.position.x <= LimiteIzquierda) Control = true;
    }
}