using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class RayCaster : MonoBehaviour
{
    public Transform spawnRay;
    public float rangeRayLight;

    public static event Action<RaycastHit, RayCaster> OnHitRayAction;
    public UnityEvent<RaycastHit> OnHitRayEvent;

    public static event Action<RayCaster> OutHitRayAction;

    public void CheckRay(LayerMask layer)
    {
        RaycastHit hit;

        if (Physics.Raycast(spawnRay.transform.position, spawnRay.transform.forward, out hit, rangeRayLight, layer))
        {
            if (OnHitRayAction != null)
                OnHitRayAction(hit, this);

            OnHitRayEvent?.Invoke(hit);
        }
        else
        {
            OutHitRayAction(this);
        }
    }

    public void CheckRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(spawnRay.transform.position, spawnRay.transform.forward, out hit, rangeRayLight))
        {
            if (OnHitRayAction != null)
                OnHitRayAction(hit, this);

            OnHitRayEvent?.Invoke(hit);
        }
        else
        {
            OutHitRayAction(this);
        }
    }
}
