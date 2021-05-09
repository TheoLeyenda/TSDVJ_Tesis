using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_ItemChecker : MonoBehaviour
{
    private J_Inventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<J_Inventory>();
    }

    public bool checkPlayerHasItem(string itemToCheckName)
    {
        return playerInventory.PlayerHasItem(itemToCheckName);
    }
}
