using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float velocidad = 2.0f;
    public float movimientoDistancia = 3.0f;
    public GameObject zombiePrefab;
    public float tiempoGeneracion = 2.0f;
    private float posicionInicialX;
    private float tiempoSiguienteGeneracion;
    private int zombiesGenerados = 0;
    public int maxZombies = 5;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicialX = transform.position.x;
        tiempoSiguienteGeneracion = Time.time + tiempoGeneracion;
    }

    // Update is called once per frame
    void Update()
    {
        float nuevaPosicionX = posicionInicialX + Mathf.Sin(Time.time * velocidad) * movimientoDistancia;
        transform.position = new Vector3(nuevaPosicionX, transform.position.y, transform.position.z);

        // Genera zombies hasta alcanzar el límite
        if (Time.time >= tiempoSiguienteGeneracion && zombiesGenerados < maxZombies)
        {
            GenerarZombie();
            tiempoSiguienteGeneracion = Time.time + tiempoGeneracion;
        }
    }
    void GenerarZombie()
    {
        Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        zombiesGenerados++;
    }
}
