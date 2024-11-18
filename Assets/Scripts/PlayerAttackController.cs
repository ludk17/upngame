using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject kunaiPrefab;   
    private GameManagerController gameManagerController;

    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManagerController = GameObject.Find("GameManager").GetComponent<GameManagerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.X) && gameManagerController.getKunais() > 0)
        {
            FirstAttack();            
        }
        
    }

    public void FirstAttack () {
        GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
        kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
        gameManagerController.ReduceKunai();
    }
}
