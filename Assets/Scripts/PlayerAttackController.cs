using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public AudioClip ataqueSonido;
    public GameObject kunaiPrefab;
    private GameManagerController gameManagerController;

    private AudioSource audioSource;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameManagerController = GameObject.Find("GameManager").GetComponent<GameManagerController>();

        // Verificar si AudioSource está asignado
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró el componente AudioSource en el GameObject.");
            audioSource = gameObject.AddComponent<AudioSource>(); // Añadir componente si no existe
        }

        // Verificar si el AudioClip está asignado
        if (ataqueSonido == null)
        {
            Debug.LogError("El AudioClip ataqueSonido no está asignado.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && gameManagerController.getKunais() > 0)
        {
            GameObject kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
            kunai.GetComponent<KunaiController>().SetDirection(sr.flipX ? "left" : "right");
            gameManagerController.ReduceKunai();

            if (ataqueSonido != null && audioSource != null)
            {
                audioSource.PlayOneShot(ataqueSonido);
            }
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            Debug.Log("Add Kunai");
            gameManagerController.AddKunai(5);
        }
    }
}
