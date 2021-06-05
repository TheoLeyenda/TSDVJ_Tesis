using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class J_Inventory : Updateable
{
    public GameObject slotParent;
    public GameObject inventoryUI;
    public int inventorySize = 16;

    List<J_Item> inventory;
    J_InventorySlot[] slots;
    private GameManager gameManagerRef;

    [SerializeField] private UnityEvent EventInventoryOn;
    [SerializeField] private UnityEvent EventInventoryOff;

    protected override void Start()
    {
        gameManagerRef = FindObjectOfType<GameManager>();

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
            if (inventoryUI.activeSelf)
                EventInventoryOn?.Invoke();
            else
                EventInventoryOff?.Invoke();

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

    public List<J_Item> GetInventory()
    {
        return inventory;
    }
}
