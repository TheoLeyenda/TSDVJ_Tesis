using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionForSpawnObjectInPosition : MonoBehaviour
{
    [SerializeField] private J_Item objectRequire;
    [SerializeField] private GameObject objectSpawn;
    [SerializeField] private Transform spawn;
    [SerializeField] private bool useParent;
    [SerializeField] private UnityEvent OnGiveObject;

    public bool CheckGiveMyObject()
    {
        if (J_inventoryManager.instance.HasItem(objectRequire))
        {
            J_inventoryManager.instance.RemoveItem(objectRequire);

            if (!useParent)
                Instantiate(objectSpawn, spawn.position, spawn.rotation);
            else
                Instantiate(objectSpawn, spawn.position, spawn.rotation, spawn);

            OnGiveObject?.Invoke();

            return true;
        }
        else
        {
            return false;
        }
    }
}
