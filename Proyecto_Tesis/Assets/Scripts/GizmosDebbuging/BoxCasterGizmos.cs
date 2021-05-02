using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCasterGizmos : MonoBehaviour
{
    [SerializeField] private float maxDistance = 0;

    private Vector3 halfExtencion = Vector3.zero;
    [SerializeField] Vector3 addHalfExtencion = Vector3.zero;
    [SerializeField] private bool useAddaHalfExtencion = false;

    void OnDrawGizmos()
    {
        RaycastHit hit;

        if (useAddaHalfExtencion)
        {
            halfExtencion = Vector3.zero;
            halfExtencion = addHalfExtencion + transform.localScale / 2;
        }
        else
        { 
            halfExtencion = transform.localScale;
        }
        bool isHit = Physics.BoxCast(transform.position, halfExtencion, transform.forward, out hit, transform.rotation, maxDistance);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, halfExtencion);
        }
    }
}
