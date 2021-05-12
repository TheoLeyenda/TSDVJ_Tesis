using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Inventory : Updateable
{
    public GameObject slotParent;
    public GameObject inventoryUI;
    public int inventorySize = 24;

    List<J_Item> inventory;
    J_InventorySlot[] slots;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateInventoryKey);
        UM.UpdatesInGame.Add(MyUpdate);

        inventory = new List<J_Item>();
        slots = slotParent.GetComponentsInChildren<J_InventorySlot>();
    }

    private void UpdateInventoryKey()
    {
        if (Input.GetButtonDown("OpenInventory"))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.activeSelf);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Count)
                slots[i].AddItem(inventory[i]);
            else
                slots[i].ClearSlot();
        }
    }

    public void AddItem(J_Item item)
    {
        if (inventory.Count < inventorySize)
        {
            inventory.Add(item);
            UpdateUI();
        }
    }

    public bool PlayerHasItem(string itemName)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == itemName)
                return true;
        }
        return false;
    }

    public void RemoveItem(J_Item item)
    {
        if (inventory.Count > 0)
        {
            inventory.Remove(item);
            UpdateUI();
        }
    }

    public bool GetInventoryFull()
    {
        return inventory.Count >= inventorySize;
    }
}
