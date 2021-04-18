using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmitingSound : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private Transform centerBoxOverlapCaster = null;
    [SerializeField] private float heightEmmitingSound = 0;
    private Vector3 halfExtents = Vector3.zero;
    private Collider[] colliders = null;

    public void ShootEmmitingSound(float valueSound)
    {
        halfExtents = new Vector3(valueSound, heightEmmitingSound, valueSound);
        colliders = Physics.OverlapBox(centerBoxOverlapCaster.position, halfExtents);

        ListeningSound listening;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null)
            {
                listening = colliders[i].GetComponent<ListeningSound>();
                if(listening != null)
                {
                    listening.OnListeningSound?.Invoke();
                }
            }
        }

        //Debug.Log("Overlap size: " + valueSound);
    }

    public Collider[] GetCurrentColliders()
    {
        return colliders;
    }
}
