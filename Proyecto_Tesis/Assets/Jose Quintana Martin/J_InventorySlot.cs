using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_InventorySlot : MonoBehaviour
{
    public Image icon;

    J_Item item;

    public void AddItem(J_Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
