using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que se va a instanciar
    public float spawnInterval = 2f; // Intervalo de tiempo entre cada aparición

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Instancia un nuevo enemigo en la posición del generador
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }


}
