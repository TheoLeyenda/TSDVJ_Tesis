using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_ItemPickUp : J_Interactable
{
    public J_Item item;

    public void AddItemToInventory() {
        if (J_inventoryManager.instance.IsInventoryFull())
            return;

        J_inventoryManager.instance.AddItem(item);

        Destroy(gameObject);
    }

    public override void Interact()
    {
        base.Interact();

        AddItemToInventory();
    }
}
