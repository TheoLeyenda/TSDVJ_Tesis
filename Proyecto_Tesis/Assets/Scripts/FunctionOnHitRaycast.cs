using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FunctionOnHitRaycast : Updateable
{
    // Start is called before the first frame update

    [SerializeField] private float range = 100.0f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private string tagTarget;
    [SerializeField] private UnityEvent functionOnHit;
    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateFunctionOnHitRaycast);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    // Update is called once per frame
    void UpdateFunctionOnHitRaycast()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, mask))
        {
            if (hit.collider.tag == tagTarget)
            {
                functionOnHit?.Invoke();
            }
        }
    }
}
