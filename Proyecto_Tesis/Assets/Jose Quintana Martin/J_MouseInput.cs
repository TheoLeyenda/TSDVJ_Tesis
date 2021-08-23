using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_MouseInput : Updateable
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputAxisMouseX = "Mouse X";
    [SerializeField] private string nameInputAxisMouseY = "Mouse Y";

    private bool enableMouseInput = true;

    private Vector2 turn;

    public float sensitivity = 2f;
    public Transform playerBody;
    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateLookAround);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    void UpdateLookAround()
    {
        if (enableMouseInput)
        {
            float AxisX = 0;
            float AxisY = 0;

            inputManager.GetInputFunction(nameInputAxisMouseX).GetAxisValue(ref AxisX);
            inputManager.GetInputFunction(nameInputAxisMouseY).GetAxisValue(ref AxisY);

            turn.x += AxisX * sensitivity;
            turn.y += AxisY * sensitivity;

            if (turn.y > 90)
            {
                turn.y = 90;
            }

            if (turn.y < -90)
            {
                turn.y = -90;
            }

            transform.localRotation = Quaternion.Euler(-turn.y, playerBody.localRotation.y, playerBody.localRotation.z);
            playerBody.localRotation = Quaternion.Euler(playerBody.localRotation.x, turn.x, playerBody.localRotation.z); //<---------- YOUUUUUUU!!!!
        }
    }

    public void SetEnableMouseInputs(bool value) => enableMouseInput = value;
}
