using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public AudioClip kunaiSound;
    public GameObject kunaiPrefab;
    private GameManagerController gameManagerController;

    SpriteRenderer sr;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameManagerController = GameObject.Find("GameManager").GetComponent<GameManagerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.X) && gameManagerController.getKunais() > 0)
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            gameManagerController.ReduceKunai();
            audioSource.PlayOneShot(kunaiSound);


        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            Console.WriteLine("Add Kunai");
            gameManagerController.AddKunai(5);
        }

    }


}