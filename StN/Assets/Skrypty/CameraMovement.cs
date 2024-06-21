using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 ruchkamery;

    
    void Update()
    {
        transform.position = player.position + ruchkamery;
    }
}
