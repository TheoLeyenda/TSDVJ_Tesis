using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_inventoryManager : MonoBehaviour
{
    public static J_inventoryManager instance { get; private set; }

    private J_Inventory playerInventory;
    public List<J_Item> allItems;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        playerInventory = FindObjectOfType<J_Inventory>();

#if UNITY_EDITOR
        if (playerInventory == null)
            Debug.LogError("Che, no encuentro un inventario");
#endif
    }

    public bool IsInventoryFull()
    {
        return playerInventory.GetInventoryFull();
    }

    public void AddItem(J_Item item)
    {
        playerInventory.AddItem(item);
    }
    public void AddItem(int itemID)
    {
        J_Item itemToAdd = null;

        foreach (J_Item item in allItems)
        {
            if (item.ID == itemID)
            {
                itemToAdd = item;
                break;
            }
        }

        if (itemToAdd != null)
            playerInventory.AddItem(itemToAdd);
    }

    public void RemoveItem(J_Item item)
    {
        playerInventory.RemoveItem(item);
    }

    public bool HasItem(J_Item item)
    {
        return playerInventory.PlayerHasItem(item);
    }

    public int GetInventorySize()
    {
        return playerInventory.inventorySize;
    }

    public int GetInventoryCount()
    {
        return playerInventory.GetInventoryCurrentCount();
    }

    public int[] GetItemsIDS()
    {
        J_InventorySlot[] slots = playerInventory.GetSlotsInventory();
        int[] itemIDS = new int[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetMyItem() != null)
            {
                if (slots[i].GetMyItem().ID != 0) //El cero es invalido en las ID de los objetos, cosas de arrays (y de que soy un pelotudo)
                {
                    itemIDS[i] = slots[i].GetMyItem().ID;
                }
            }
        }

        return itemIDS;
    }
}
