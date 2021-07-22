using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_inventoryManager : MonoBehaviour
{
    public static J_inventoryManager instance { get; private set; }

    private J_Inventory playerInventory;

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

    public bool IsInventoryFull() {
        return playerInventory.GetInventoryFull();
    }

    public void AddItem(J_Item item) {
        playerInventory.AddItem(item);
    }

    public void RemoveItem(J_Item item) {
        playerInventory.RemoveItem(item);
    }

    public bool HasItem(J_Item item) {
        return playerInventory.PlayerHasItem(item);
    }

    public int GetInventorySize() {
        return playerInventory.inventorySize;
    }

    public int GetInventoryCount()
    {
        return playerInventory.GetInventoryCurrentCount();
    }
}
