using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject kunaiPrefab;   
    private GameManagerController gameManagerController;
    private bool kunai = false;
    public float cooldownTime = 1f; 
    private float lastThrowTime;

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

        if (kunai && Time.time >= lastThrowTime + cooldownTime)
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            gameManagerController.ReduceKunai();

            lastThrowTime = Time.time;
            End();
        }


    }

    public void LanzarKunai()
    {
        kunai = true;
    }

    public void End()
    {
        kunai = false;
    }
}
