using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_MouseInput : MonoBehaviour
{
    public Vector2 turn;

    public float sensitivity = 2f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        if (turn.y > 90)
        {
            turn.y = 90;
        }

        if (turn.y < -90)
        {
            turn.y = -90;
        }

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
