using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_ItemPickUp : J_Interactable
{
    public J_Inventory playerInventory;
    public J_Item item;

    private void Start()
    {
        playerInventory = FindObjectOfType<J_Inventory>();
    }

    public void AddItemToInventory() {
        if (playerInventory.GetInventoryFull())
            return;

        playerInventory.AddItem(item);

        Destroy(gameObject);
    }

    public override void Interact()
    {
        base.Interact();

        AddItemToInventory();
    }
}
