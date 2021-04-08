using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCasterGizmos : MonoBehaviour
{
    [SerializeField] private float maxDistance;

    [SerializeField] float radiusSphere;

    void OnDrawGizmos()
    {
        RaycastHit hit;

        bool isHit = Physics.SphereCast(transform.position, radiusSphere,transform.forward, out hit, maxDistance);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance, radiusSphere);
        }
    }
}
