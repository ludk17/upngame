using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisoChoque : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip caidaSonido;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ReproducirSonidoCaida();
        }
    }

    private void ReproducirSonidoCaida()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(caidaSonido);
        }
    }
}
