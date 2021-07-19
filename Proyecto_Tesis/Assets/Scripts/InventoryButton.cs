using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private J_InventorySlot j_InventorySlot;

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
        j_InventorySlot.DisplayInfoItem();
    }

    public void OnDeselect(BaseEventData data)
    {
        //Debug.Log("Deselected");
        j_InventorySlot.DestroyUI();
    }

}
