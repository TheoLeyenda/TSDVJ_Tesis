using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastGizmos : MonoBehaviour
{
    [SerializeField] private float rangeRay = 30;
    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.forward;
        Gizmos.DrawRay(transform.position, direction * rangeRay);
    }
}
