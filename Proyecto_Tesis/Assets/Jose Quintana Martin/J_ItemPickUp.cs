using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_ItemPickUp : MonoBehaviour
{
    public J_Inventory playerInventory;
    public J_Item item;

    private void Start()
    {
        playerInventory = FindObjectOfType<J_Inventory>();
    }

    public void AddItemToInventory() {
        playerInventory.AddItem(item);
        Debug.Log("Added " + item.itemName + " to inventory");

        Destroy(gameObject);
    }
}
