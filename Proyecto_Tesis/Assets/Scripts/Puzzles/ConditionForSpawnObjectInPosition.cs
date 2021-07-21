using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConditionForSpawnObjectInPosition : MonoBehaviour
{
    [SerializeField] private J_Item objectRequire;
    [SerializeField] private GameObject objectSpawn;
    [SerializeField] private Transform spawn;
    [SerializeField] private J_Inventory inventoryPlayer;
    [SerializeField] private bool useParent;
    [SerializeField] private UnityEvent OnGiveObject;

    public bool CheckGiveMyObject()
    {
        if (inventoryPlayer.PlayerHasItem(objectRequire))
        {
            inventoryPlayer.RemoveItem(objectRequire);

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
