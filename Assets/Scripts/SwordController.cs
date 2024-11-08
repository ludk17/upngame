using UnityEngine;

public class SwordController : MonoBehaviour
{
    GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destruye al enemigo
            gameManager.GetComponent<GameManagerController>().AddScore(10);
        }
    }
}
