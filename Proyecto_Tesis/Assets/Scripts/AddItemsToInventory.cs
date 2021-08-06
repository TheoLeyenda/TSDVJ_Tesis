using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AddItemsToInventory : MonoBehaviour
{
    [SerializeField] private List<J_Item> j_items;
    [SerializeField] private UnityEvent OnFinishAddItems; 
    public void AddItems(bool removeObjectsToList)
    {
        bool doneAddItems = true;
        if (removeObjectsToList)
        {
            for (int i = 0; i < j_items.Count; i++)
            {
                bool doneAddItem = !J_inventoryManager.instance.IsInventoryFull();
                if (doneAddItem)
                {
                    J_inventoryManager.instance.AddItem(j_items[i]);
                    j_items.Remove(j_items[i]);
                    i--;
                }

            }
            if (j_items.Count > 0)
                doneAddItems = false;
        }
        else
        {
            if (J_inventoryManager.instance.GetInventoryCount() + j_items.Count < J_inventoryManager.instance.GetInventorySize())
            {
                for (int i = 0; i < j_items.Count; i++)
                {
                    J_inventoryManager.instance.AddItem(j_items[i]);
                }
            }
            else
            {
                doneAddItems = false;
            }
        }
        if (doneAddItems)
        {
            OnFinishAddItems?.Invoke();
        }
    }
}
