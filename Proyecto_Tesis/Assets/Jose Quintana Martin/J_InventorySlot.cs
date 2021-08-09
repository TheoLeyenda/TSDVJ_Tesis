using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
public class J_InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Button button;
    public EventSystem eventSystem;

    [SerializeField] private GameObject DisplayerIconComponenObject;
    [SerializeField] private GameObject DisplayIconSimpleObject;

    J_Item item;
    J_ItemDisplay display;

    public static event Action<J_InventorySlot> OnClearSlot;

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
        if (item != null) {
            item.InvokeFunctionItem();
            Debug.Log("He realizado la funcion: " + item.itemName);
        }
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
        DestroyUI();
        //Debug.Log("1");
        if (OnClearSlot != null)
        {
            //Debug.Log("2");
            OnClearSlot(this);
        }
    }

    public void DisplayInfoItem()
    {
        if (item != null)
        {
            display.UpdateDescription(item.description);
            display.UpdateName(item.itemName);
            if (item.useIconCompound)
            {
                DisplayerIconComponenObject.SetActive(true);
                DisplayIconSimpleObject.SetActive(false);
                display.UpdateImage(item.iconsCompound);
            }
            else
            {
                DisplayIconSimpleObject.SetActive(true);
                DisplayerIconComponenObject.SetActive(false);
                display.UpdateImage(item.icon);
            }
            eventSystem.firstSelectedGameObject = button.gameObject;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayInfoItem();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyUI();
    }
    public void DestroyUI()
    {
        if (display != null)
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

    public J_Item GetMyItem() { return item; }
}
