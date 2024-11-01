using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Necesario para usar PointerDown y PointerUp

public class PlayerInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerController playerController;
    private bool isMovingLeft;
    private bool isMovingRight;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        GameObject.Find("BotonIzquierda").GetComponent<Button>().onClick.AddListener(MoveLeft);
        GameObject.Find("BotonDerecha").GetComponent<Button>().onClick.AddListener(MoveRight);
        GameObject.Find("BotonDisparo").GetComponent<Button>().onClick.AddListener(Attack);
    }

    void Update()
    {
        if (isMovingLeft)
        {
            playerController.MoveLeft();
        }
        else if (isMovingRight)
        {
            playerController.MoveRight();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Identifica qué botón se ha presionado y comienza el movimiento
        if (eventData.pointerPress.name == "BotonIzquierda")
        {
            isMovingLeft = true;
        }
        else if (eventData.pointerPress.name == "BotonDerecha")
        {
            isMovingRight = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Detiene el movimiento cuando se suelta el botón
        isMovingLeft = false;
        isMovingRight = false;
    }

    public void MoveLeft()
    {
        isMovingLeft = true;
        isMovingRight = false;
    }

    public void MoveRight()
    {
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void Attack()
    {
        playerController.Attack();
    }
}
