using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Movement : Updateable
{
    public float speed;
    public float walkingSoundIntensity = 7f;
    public EmmitingSound sound;

    [SerializeField] private Stuneable stuneable;
    [SerializeField] private Transform myUserTransform;

    private CharacterController cc;
    private float xMov;
    private float zMov;
    private float auxSpeed;

    protected override void OnEnable()
    {
        base.OnEnable();
        Monster.OnSendStuneTargetEvent += CheckInStuneMovmement;
        Monster.OnOutStuneTarget += CheckOutStuneMovement;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Monster.OnSendStuneTargetEvent -= CheckInStuneMovmement;
        Monster.OnOutStuneTarget -= CheckOutStuneMovement;
    }

    protected override void Start()
    {
        auxSpeed = speed;
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

        if (stuneable.GetInStune())
        {
            stuneable.CheckDelayStune(stuneable.GetDelayStune(),ref speed, auxSpeed);
            stuneable.SetDelayStune(stuneable.GetDelayStune() - Time.deltaTime);
        }

    }

    private void CheckInStuneMovmement(Transform _transform, float delayStune)
    {
        if(myUserTransform == _transform)
        {
            stuneable.SetInStune(true);
            stuneable.SetDelayStune(delayStune);
        }
    }

    private void CheckOutStuneMovement(Transform _transform)
    {
        if (myUserTransform == _transform)
        {
            stuneable.SetDelayStune(0.0f);
        }
    }
}
