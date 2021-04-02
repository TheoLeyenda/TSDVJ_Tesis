using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFunctions : MonoBehaviour
{
    [SerializeField] private float impulse;
    [SerializeField] private Rigidbody rig;
    [SerializeField] private bool useCheckAviableJump = true;
    [SerializeField] private float rangeRayCheckIsFloor;
    private GetDirectionForEnum getDirectionForEnum;

    bool aviableJump = true;

    void Start()
    {
        getDirectionForEnum = new GetDirectionForEnum();
    }

    public void Jump(int directionUse)
    {
        Vector3 directionUp = getDirectionForEnum.GetVectorDirection((GetDirectionForEnum.Direction)directionUse);

        if (!useCheckAviableJump || aviableJump && (CheckAviableJump(-directionUp)))
        {
            if (rig != null)
                rig.AddForce(directionUp * (impulse * Time.deltaTime), ForceMode.Impulse);
            else
                Debug.Log("rig is null");
        }
    }

    private bool CheckAviableJump(Vector3 direction)
    {
        RaycastHit hit;
        aviableJump = false;
        bool isFloor = Physics.Raycast(transform.position, direction, out hit, rangeRayCheckIsFloor);
        return isFloor;
    }

    void OnCollisionEnter(Collision collision)
    {
        aviableJump = true;
        if (rig != null)
        {
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }

    }

}
