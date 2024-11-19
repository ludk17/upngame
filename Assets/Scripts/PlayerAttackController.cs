using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public AudioClip atack;

    public GameObject kunaiPrefab;   
    private GameManagerController gameManagerController;

    private AudioSource audioSource;
   
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManagerController = GameObject.Find("GameManager").GetComponent<GameManagerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.X) && gameManagerController.getKunais() > 0)
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            audioSource.PlayOneShot(atack);
            gameManagerController.ReduceKunai();
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            Console.WriteLine("Add Kunai");
            gameManagerController.AddKunai(5);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            Console.WriteLine("Add Life");
            gameManagerController.AddLife();
        }
    }    
}
