using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOverlapGizmos : MonoBehaviour
{
    [SerializeField] private Vector3 halfExtencion = Vector3.zero;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, halfExtencion);
    }
}
