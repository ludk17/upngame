using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KunaiController : MonoBehaviour
{
    public float defaultVelocityX = 5f;
    float currentVelocityX = 0;
    Rigidbody2D rb;
    GameObject gameManager;
    public Button launchButton;

    public Button LaunchButton { get => launchButton; set => launchButton = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");

        // Asignar el método LaunchKunai al evento del botón
        LaunchButton.onClick.AddListener(LaunchKunai);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(currentVelocityX, rb.velocity.y);
    }

    public void SetDirection(string direction)
    {
        if (direction == "left")
        {
            currentVelocityX = -defaultVelocityX;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            currentVelocityX = defaultVelocityX;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    // Método para lanzar kunai
    public void LaunchKunai()
    {
        // Implementa la lógica para lanzar el kunai aquí
        // Por ejemplo, puedes cambiar la velocidad o aplicar una fuerza
        rb.velocity = new Vector2(currentVelocityX, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            gameManager.GetComponent<GameManagerController>().AddScore(10);
        }
    }
}
