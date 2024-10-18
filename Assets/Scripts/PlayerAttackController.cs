using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject kunaiPrefab;   
    private GameManagerController gameManagerController;
    public AudioClip sound;
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

        if (Input.GetKeyUp(KeyCode.X))
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            gameManagerController.ReduceKunai();
            audioSource.PlayOneShot(sound);
        }
        
    }

    
}
