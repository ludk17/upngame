using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controladorenemigos : MonoBehaviour
{
    public GameObject Zombie; // Prefab del zombi
    public float intervaloGeneracion = 2f; // Tiempo entre generacion de zombis
    private float tiempoDesdeGeneracion = 0f; // Temporizador

    void Update()
    {
        tiempoDesdeGeneracion += Time.deltaTime;

        if (tiempoDesdeGeneracion >= intervaloGeneracion)
        {
            GenerarZombi();
            tiempoDesdeGeneracion = 0f; // Reiniciar el temporizador
        }
    }

    void GenerarZombi()
    {
        // Instanciar un nuevo zombi en la posición de la nave
        Instantiate(Zombie, transform.position, Quaternion.identity);
    }
}
