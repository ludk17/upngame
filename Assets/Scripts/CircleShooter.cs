using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del sprite que será disparado
    public float shootForce = 10f;      // Fuerza con la que serán disparados los proyectiles
    public float shootInterval = 1f;    // Intervalo de tiempo entre cada disparo

    private void Start()
    {
        // Inicia la rutina para disparar continuamente
        InvokeRepeating("ShootProjectile", 0f, shootInterval);
    }

    void ShootProjectile()
    {
        // Instanciar el proyectil en la posición del círculo
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Añadir una fuerza para que el proyectil se mueva
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = Random.insideUnitCircle.normalized; // Dirección aleatoria
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
        }
    }
}
