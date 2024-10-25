using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientonave : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener el input del usuario
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        // Calcular el movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, 0) * velocidad * Time.deltaTime;

        // Aplicar el movimiento a la nave
        transform.Translate(movimiento);
    }
}
