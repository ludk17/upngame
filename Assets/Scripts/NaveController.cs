using UnityEngine;

public class NaveController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2f;        // Velocidad de movimiento
    public float rangoMinX = -5f;       // Límite izquierdo
    public float rangoMaxX = 5f;        // Límite derecho

    [Header("Generación de Zombies")]
    public GameObject zombiePrefab;      // Prefab del zombie
    public float intervaloSpawn = 5f;    // Intervalo entre spawn de zombies

    private bool moviendoDerecha = true;
    private float tiempoUltimoSpawn;

    void Start()
    {
        tiempoUltimoSpawn = Time.time;
    }

    void Update()
    {
        MoverNave();

        // Generar zombies cada 5 segundos
        if (Time.time - tiempoUltimoSpawn >= intervaloSpawn)
        {
            GenerarZombie();
            tiempoUltimoSpawn = Time.time;
        }
    }

    void MoverNave()
    {
        // Obtener posición actual
        Vector3 posicion = transform.position;

        // Calcular nueva posición
        if (moviendoDerecha)
        {
            posicion.x += velocidad * Time.deltaTime;
            if (posicion.x >= rangoMaxX)
            {
                moviendoDerecha = false;
            }
        }
        else
        {
            posicion.x -= velocidad * Time.deltaTime;
            if (posicion.x <= rangoMinX)
            {
                moviendoDerecha = true;
            }
        }

        // Aplicar nueva posición
        transform.position = posicion;
    }

    void GenerarZombie()
    {
        // Crea un zombie en la posición de la nave
        if (zombiePrefab != null)
        {
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        }
    }
}