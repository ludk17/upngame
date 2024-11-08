using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public AudioClip atack;
    public AudioClip attackSword;
    public GameObject kunaiPrefab;
    public GameObject swordHitBox; // Caja de colisión para el ataque de espada
    private GameManagerController gameManagerController;

    private AudioSource audioSource;



    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManagerController = GameObject.Find("GameManager").GetComponent<GameManagerController>();
       
        audioSource = gameObject.AddComponent<AudioSource>();
        swordHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
    // Función pública para iniciar el ataque
    public void Attack()
    {
        StartCoroutine(PerformAttack());
    }
    public void AttackKunai()
    {
        if (gameManagerController.getKunais() > 0)
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            audioSource.PlayOneShot(atack);
            gameManagerController.ReduceKunai();
        }
    }
    private IEnumerator PerformAttack()
    {
        audioSource.PlayOneShot(attackSword);
        swordHitBox.SetActive(true); 
        yield return new WaitForSeconds(0.5f); 
        swordHitBox.SetActive(false); 
    }
}
