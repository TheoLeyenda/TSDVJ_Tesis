using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Movement : Updateable
{
    public float speed;

    private CharacterController cc;
    private float xMov;
    private float zMov;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateMovement);
        UM.UpdatesInGame.Add(MyUpdate);

        cc = GetComponent<CharacterController>();
    }

    public void UpdateMovement()
    {
        xMov = Input.GetAxis("Horizontal");
        zMov = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xMov + transform.forward * zMov;

        cc.Move(move * speed * Time.deltaTime);
    }


}
