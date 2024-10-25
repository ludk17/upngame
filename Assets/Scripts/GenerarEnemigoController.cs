using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarEnemigoController : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public float tiempoGeneracion = 3f;  // Tiempo en segundos entre cada generación
    private float contadorTiempo;
    public int limiteEnemigos = 5;  // Límite de enemigos en escena
    public float rangoGeneracionX = 0f; 

    private List<GameObject> enemigosActivos = new List<GameObject>();  

    // Start is called before the first frame update
    void Start()
    {
        contadorTiempo = tiempoGeneracion;  
    }

    // Update is called once per frame
    void Update()
    {
        
        if (enemigosActivos.Count < limiteEnemigos)
        {
            contadorTiempo -= Time.deltaTime; 

            if (contadorTiempo <= 0f)
            {
                
                Vector3 posicionGeneracion = transform.position;
                posicionGeneracion.x += Random.Range(-rangoGeneracionX, rangoGeneracionX);

                
                GameObject nuevoEnemigo = Instantiate(enemigoPrefab, posicionGeneracion, Quaternion.identity);
                enemigosActivos.Add(nuevoEnemigo);

                
                contadorTiempo = tiempoGeneracion;
            }
        }

        
        enemigosActivos.RemoveAll(enemigo => enemigo == null);  
    }
}
