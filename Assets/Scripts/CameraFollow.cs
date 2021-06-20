using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void LateUpdate()
    {
        Vector3 temp = transform.position;//store current position of camera (it must be a variable)

        //set camera's position x to equal to players position
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;
        //set camera position back to camera's current position
        transform.position = temp;
    }
}
