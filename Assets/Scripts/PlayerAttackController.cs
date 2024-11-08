using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject mainPlayer;
    public Button kunaiButton;
    public GameObject kunaiPrefab;   
    private GameManagerController gameManagerController;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManagerController = GameObject.Find("GameManager").GetComponent<GameManagerController>();
        // Asignar el método LaunchKunai al evento del botón
        kunaiButton.onClick.AddListener(LaunchKunai);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && gameManagerController.getKunais() > 0)
        {
            LaunchKunai();
        }
    }

    // Método para lanzar kunai
    public void LaunchKunai()
    {
        if (gameManagerController.getKunais() > 0)
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            gameManagerController.ReduceKunai();
        }
    }
}
