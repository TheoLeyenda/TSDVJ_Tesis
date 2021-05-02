using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFunctions : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    private float auxSpeed = 0;
    [SerializeField] private Rigidbody rig = null;
    private GetDirectionForEnum getDirectionForEnum = null;

    void Start()
    {
        getDirectionForEnum = new GetDirectionForEnum();
        auxSpeed = speed;
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void ResetSpeed()
    {
        speed = auxSpeed;
    }
    public void MovementForPosition(int directionUse)
    {
        Vector3 direction = getDirectionForEnum.GetVectorDirection((GetDirectionForEnum.Direction)directionUse);
        transform.position += direction * (speed * Time.deltaTime);
    }

    public void MovementForAceleration(int directionUse)
    {
        MovementForForces(directionUse, ForceMode.Acceleration);
    }

    public void MovementForForce(int directionUse)
    {
        MovementForForces(directionUse, ForceMode.Force);
    }

    public void MovementForImpulse(int directionUse)
    {
        MovementForForces(directionUse, ForceMode.Impulse);
    }

    public void MovementForVelocityChange(int directionUse)
    {
        MovementForForces(directionUse, ForceMode.VelocityChange);
    }

    public void MovementForForces(int directionUse, ForceMode forceMode)
    {
        Vector3 direction = getDirectionForEnum.GetVectorDirection((GetDirectionForEnum.Direction)directionUse);
        if (rig != null)
            rig.AddForce(direction * (speed * Time.deltaTime), forceMode);
        else
            Debug.Log("rig is null");
    }

    public void StopMovementForces()
    {
        if (rig != null)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }
        else
            Debug.Log("rig is null");
    }

    public void SetSpeed(float value) => speed = value;

    public ref float GetSpeed() { return ref speed; }

    public float GetAuxSpeed() { return auxSpeed; }

}
