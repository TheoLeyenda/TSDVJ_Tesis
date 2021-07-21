using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class J_InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Button button;

    J_Item item;
    J_ItemDisplay display;

    void OnDisable()
    {
        DestroyUI();
    }

    private void Start()
    {
        display = FindObjectOfType<J_ItemDisplay>();
        if (display)
        {
            Debug.Log("Display found");
        }
        else
        {
            Debug.Log("Display NOT found");
        }
    }

    public void UseItem()
    {
        if(item != null)
            item.InvokeFunctionItem();

        Debug.Log("Me han realizado la clickeacion: " + item.itemName);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            display.UpdateDescription(item.description);
            display.UpdateName(item.itemName);
            if (item.useIconCompound)
                display.UpdateImage(item.iconsCompound);
            else
                display.UpdateImage(item.icon);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyUI();
    }
    public void DestroyUI()
    {
        Sprite s = display.defaultSprite;
        display.UpdateDescription(null);
        display.UpdateName(null);
        display.UpdateImage(s);
        if (item != null)
        {
            if (item.useIconCompound)
                display.DestroyImagesIcons();
        }
    }
}
