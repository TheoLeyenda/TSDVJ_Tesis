using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Movement : Updateable
{
    public float speed;
    public float walkingSoundIntensity = 7f;
    public EmmitingSound sound;

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
        Vector3 move = transform.right * xMov + transform.forward * zMov;

        xMov = Input.GetAxis("Horizontal");
        zMov = Input.GetAxis("Vertical");

        if (xMov != 0 || zMov !=0)
        {
            cc.Move(move * speed * Time.deltaTime);

            sound.ShootEmmitingSound(walkingSoundIntensity);
        }
        else
        {
            sound.ShootEmmitingSound(0f);
        }
    }
}
