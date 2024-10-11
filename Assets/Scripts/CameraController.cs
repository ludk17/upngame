using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("MainPlayer");   
    }

    // Update is called once per frame
    void Update()
    {   
        float x = player.transform.position.x + 10;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
