using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HaveItemsInInventoryEvent : MonoBehaviour
{
    void Start() { }

    [SerializeField] private List<J_Item> requestedItems;
    [SerializeField] private bool removeRequestedItems = true;
    [SerializeField] private UnityEvent eventHaveItemsInInventory;
    [SerializeField] private UnityEvent eventNotHaveItemsInInventory;

    public void CheckHaveItemInInventoryEvent()
    {
        bool hasItems = true;
        for (int i = 0; i < requestedItems.Count; i++)
        {
            hasItems = J_inventoryManager.instance.HasItem(requestedItems[i]);

            if (!hasItems)
            {
                eventNotHaveItemsInInventory.Invoke();
                return;
            }
        }

        eventHaveItemsInInventory.Invoke();
        if (removeRequestedItems)
        {
            for (int i = 0; i < requestedItems.Count; i++)
            {
                J_inventoryManager.instance.RemoveItem(requestedItems[i]);
            }
        }
    }
}
