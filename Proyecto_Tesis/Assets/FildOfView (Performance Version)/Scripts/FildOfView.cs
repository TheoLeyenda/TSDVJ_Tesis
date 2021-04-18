using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FildOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public static event Action<Transform, FildOfView> OnViewTargetWhitFildOfViewCheck;
    public static event Action<Transform> OnViewTarget;

    public List<Transform> visibleTargets = new List<Transform>();

    public void ClearVisibleTargets()
    {
        visibleTargets.Clear();
    }

    public void FindVisibleTargets()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle/2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);

                    if (OnViewTarget != null)
                        OnViewTarget(target);

                    if (OnViewTargetWhitFildOfViewCheck != null)
                        OnViewTargetWhitFildOfViewCheck(target, this);
                }
            }
        }
    }

    public Vector3 DirFormAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
