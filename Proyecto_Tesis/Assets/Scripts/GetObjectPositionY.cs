using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectPositionY : MonoBehaviour
{
    public float heigthGenerateRayCast;
    public float heightModifire = 4;
    public float rangeRayCast = 1000;

    [SerializeField] private LayerMask layerEnvarioment;

    public float GetPositionY()
    {
        RaycastHit hit;

        Vector3 positonRayCast = new Vector3(transform.position.x, heigthGenerateRayCast + transform.position.y, transform.position.z);

        float Y = 0;

        if (Physics.Raycast(positonRayCast, Vector3.down, out hit, rangeRayCast, layerEnvarioment))
        {
            Y = hit.point.y + heightModifire;
        }

        return Y;
    }
}
