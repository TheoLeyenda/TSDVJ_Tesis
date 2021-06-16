using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HaveItemsInInventoryEvent : MonoBehaviour
{
    void Start() {}

    [System.Serializable]
    public class ItemChecker
    {
        public J_Item item;
        public bool itemCheck;

        public void ResetItemCheck()
        {
            itemCheck = false;
        }
    }

    [SerializeField] private List<ItemChecker> itemsRequest;
    [SerializeField] private bool removeItemsRequestToInventory = true;
    [SerializeField] private J_Inventory inventory;
    [SerializeField] private UnityEvent eventHaveItemsInInventory;
    [SerializeField] private UnityEvent eventNotHaveItemsInInventory;

    public void CheckHaveItemInInventoryEvent()
    {
        bool allItemCheck = true;
        for (int i = 0; i < itemsRequest.Count; i++)
        {
            for (int j = 0; j < inventory.GetInventory().Count; j++)
            {
                if (itemsRequest[i].item == inventory.GetInventory()[j])
                {
                    itemsRequest[i].itemCheck = true;
                }
            }
        }

        for (int i = 0; i < itemsRequest.Count; i++)
        {
            if (!itemsRequest[i].itemCheck)
            {
                allItemCheck = false;
                break;
            }
        }

        for (int i = 0; i < itemsRequest.Count; i++)
        {
            itemsRequest[i].ResetItemCheck();
        }

        if (allItemCheck)
        {
            eventHaveItemsInInventory?.Invoke();

            if (removeItemsRequestToInventory)
            {
                for (int i = 0; i < itemsRequest.Count; i++)
                {
                    inventory.RemoveItem(itemsRequest[i].item);
                }
            }
        }
        else
        {
            eventNotHaveItemsInInventory?.Invoke();
        }
    }
}
