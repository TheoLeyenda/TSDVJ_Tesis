using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class J_InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button button;

    J_Item item;

    public void UseItem()
    {
        if(item != null)
            item.InvokeFunctionItem();
    }

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
