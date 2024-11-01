using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject kunaiPrefab;   
    private GameManagerController gameManagerController;
    private AudioSource audioSource;
    public AudioClip attackSound;

    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManagerController = GameObject.Find("GameManager").GetComponent<GameManagerController>();
    }

    // Update is called once per frame
    public void Attack()
    {
        if (gameManagerController.getKunais() > 0)
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            gameManagerController.ReduceKunai();
            audioSource.PlayOneShot(attackSound);
        }
    }
      
}
