using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 10.0f;

    void Update()
    {
        var direction = new Vector3();
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction.z += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.z -= 1.0f;
        }
        transform.position = transform.position + direction * Time.deltaTime * movementSpeed;
    }
}
