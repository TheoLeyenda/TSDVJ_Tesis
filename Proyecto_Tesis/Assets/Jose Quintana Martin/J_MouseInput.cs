using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_MouseInput : Updateable
{
    private Vector2 turn;

    public float sensitivity = 2f;
    public Transform playerBody;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateLookAround);
        UM.UpdatesInGame.Add(MyUpdate);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void UpdateLookAround()
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

        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        playerBody.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
}
