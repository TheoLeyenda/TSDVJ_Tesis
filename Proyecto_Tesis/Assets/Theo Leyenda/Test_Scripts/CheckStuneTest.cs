using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStuneTest : Updateable
{
    // Start is called before the first frame update
    [SerializeField] private MovementFunctions myMovementFunctions;
    [SerializeField] private Stuneable stuneable;
    [SerializeField] private Transform myUserTransform;
    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateCheckStuneTest);
        UM.UpdatesInGame.Add(MyUpdate);
    }
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
    // Update is called once per frame
    public void UpdateCheckStuneTest()
    {
        if (stuneable.GetInStune())
        {
            stuneable.CheckDelayStune(stuneable.GetDelayStune(), ref myMovementFunctions.GetSpeed(), myMovementFunctions.GetAuxSpeed());
            stuneable.SetDelayStune(stuneable.GetDelayStune() - Time.deltaTime);
        }
    }

    private void CheckInStuneMovmement(Transform _transform, float delayStune)
    {
        if (myUserTransform == _transform)
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
