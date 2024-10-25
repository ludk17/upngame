using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f; // Velocidad de movimiento
    [SerializeField] private float moveRange = 5f; // Rango de movimiento (distancia desde la posición inicial)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Guarda la posición inicial
    }

    void Update()
    {
        // Calcula el desplazamiento oscilante en el eje X
        float oscillation = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        
        // Aplica el nuevo posicionamiento
        transform.position = startPosition + new Vector3(oscillation, 0, 0);
    }
}
