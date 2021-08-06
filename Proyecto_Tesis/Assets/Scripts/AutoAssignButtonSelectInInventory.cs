using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutoAssignButtonSelectInInventory : MonoBehaviour
{
    [SerializeField] private J_Inventory inventory;
    [SerializeField] private EventSystem eventSystem;

    void OnEnable()
    {
        J_InventorySlot.OnClearSlot += AssignButtonSelect;
    }

    void OnDisable()
    {
        J_InventorySlot.OnClearSlot -= AssignButtonSelect;
    }

    void AssignButtonSelect(J_InventorySlot j_InventorySlot)
    {
        if (inventory == null || inventory.GetSlotsInventory().Length <= 0)
            return;

        
        bool enableAssignButton = false;

        for (int i = 0; i < inventory.GetSlotsInventory().Length; i++)
        {
            if (inventory.GetSlotsInventory()[i] == j_InventorySlot)
            {
                enableAssignButton = true;
                i = inventory.GetSlotsInventory().Length;
            }
        }

        Debug.Log("EnableAssignButton: " + enableAssignButton);

        if (enableAssignButton)
        {
            bool doneAssigned = false;
            for (int i = 0; i < inventory.GetSlotsInventory().Length; i++)
            {
                if (inventory.GetSlotsInventory()[i].GetMyItem() != null)
                {
                    doneAssigned = true;
                    eventSystem.firstSelectedGameObject = inventory.GetSlotsInventory()[i].button.gameObject;
                    i = inventory.GetSlotsInventory().Length;
                    //Debug.Log("ENTRE");
                }
            }

            //Debug.Log(eventSystem.firstSelectedGameObject.name);

            if (!doneAssigned)
            {
                eventSystem.firstSelectedGameObject = inventory.GetSlotsInventory()[0].gameObject;
            }
        }
    }
}
