using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionForTakeContentByGameObject : MonoBehaviour
{
    [SerializeField] private J_Item objectRequire;
    [SerializeField] private J_Item objectContent;
    [SerializeField] private J_Inventory inventoryPlayer;
    private bool giveMyObject = false;
    public void CheckGiveMyObject()
    {
        if (!giveMyObject)
        {
            if (inventoryPlayer.PlayerHasItem(objectRequire.itemName) && !inventoryPlayer.GetInventoryFull())
            {
                inventoryPlayer.RemoveItem(objectRequire);
                inventoryPlayer.AddItem(objectContent);
                giveMyObject = true;
            }
        }
    }
}
