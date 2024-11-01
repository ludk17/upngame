using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateZombie : MonoBehaviour
{
    public GameObject enemigoPrefab; 
    public Transform puntoDeGeneracion; 
    public float tiempoEntreGeneracion = 3f; 
    public float radioGeneracion = 1f; 
    public float rangoMovimiento = 5f; // Distancia de movimiento de izquierda a derecha
    public float tiempoMovimiento = 5f; // Tiempo que tarda en moverse de un extremo al otro

    private float contadorTiempo = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Actualizar el tiempo del contador para la generación de enemigos
        contadorTiempo += Time.deltaTime;

        // Generar un enemigo cada vez que el contador supera el tiempo establecido
        if (contadorTiempo >= tiempoEntreGeneracion)
        {
            GenerarEnemigo();
            contadorTiempo = 0f; // Reiniciar el contador
        }

        // Mover el generador de zombies de izquierda a derecha
        MoverGenerador();
    }

    void GenerarEnemigo()
    {
        // Calcular una posición aleatoria dentro del círculo (usando el radio)
        Vector2 posicionAleatoria = (Vector2)puntoDeGeneracion.position + Random.insideUnitCircle * radioGeneracion;

        // Instanciar el enemigo en la posición calculada
        Instantiate(enemigoPrefab, posicionAleatoria, Quaternion.identity);
    }

    void MoverGenerador()
    {
        // Movimiento de izquierda a derecha usando Mathf.PingPong
        float nuevaPosX = Mathf.PingPong(Time.time * (rangoMovimiento / tiempoMovimiento), rangoMovimiento) - (rangoMovimiento / 2);
        transform.position = new Vector3(nuevaPosX, transform.position.y, transform.position.z);
    }
}
