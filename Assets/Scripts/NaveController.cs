using UnityEngine;

public class AlienController : MonoBehaviour
{
    [Header("Configuración del Alien")]
    public float velocidadMovimiento = 5f;
    public float margenLimite = 1f;
    private float limiteIzquierdo, limiteDerecho;
    private bool moviendoseDerecha = true;

    [Header("Configuración de Disparo")]
    public GameObject zombiePrefab;
    public float intervaloDisparo = 2f;
    public float velocidadZombie = 5f;
    public Transform puntoDisparo; // Punto desde donde salen los zombies

    private float temporizadorDisparo = 0f;

    void Start()
    {
        // Calcula los límites de la pantalla
        Camera camaraPrincipal = Camera.main;
        float alturaCamara = camaraPrincipal.orthographicSize;
        float anchoCamara = alturaCamara * camaraPrincipal.aspect;

        limiteIzquierdo = -anchoCamara + margenLimite;
        limiteDerecho = anchoCamara - margenLimite;
    }

    void Update()
    {
        MoverAutomaticamente();
        DispararZombie();
    }

    void MoverAutomaticamente()
    {
        // Calcula la dirección del movimiento
        float direccion = moviendoseDerecha ? 1 : -1;

        // Mueve el alien
        Vector3 nuevaPosicion = transform.position + new Vector3(direccion * velocidadMovimiento * Time.deltaTime, 0, 0);

        // Verifica si llegó a los límites
        if (nuevaPosicion.x >= limiteDerecho)
        {
            moviendoseDerecha = false;
            nuevaPosicion.x = limiteDerecho;
        }
        else if (nuevaPosicion.x <= limiteIzquierdo)
        {
            moviendoseDerecha = true;
            nuevaPosicion.x = limiteIzquierdo;
        }

        // Actualiza la posición
        transform.position = nuevaPosicion;
    }

    void DispararZombie()
    {
        temporizadorDisparo += Time.deltaTime;

        if (temporizadorDisparo >= intervaloDisparo)
        {
            // Crea el zombie en el punto de disparo
            GameObject nuevoZombie = Instantiate(zombiePrefab, puntoDisparo.position, Quaternion.identity);

            // Configura el movimiento del zombie
            Rigidbody2D rbZombie = nuevoZombie.GetComponent<Rigidbody2D>();
            if (rbZombie != null)
            {
                rbZombie.velocity = Vector2.down * velocidadZombie;
            }

            temporizadorDisparo = 0f;
        }
    }
}