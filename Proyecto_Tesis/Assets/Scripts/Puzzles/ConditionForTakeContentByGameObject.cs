using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ConditionForTakeContentByGameObject : MonoBehaviour
{
    [SerializeField] private J_Item objectRequire;
    [SerializeField] private J_Item objectContent;
    [SerializeField] private J_Inventory inventoryPlayer;
    [SerializeField] private UnityEvent OnGiveObject;
    private bool giveMyObject = false;
    public void CheckGiveMyObject()
    {
        if (!giveMyObject)
        {
            if (inventoryPlayer.PlayerHasItem(objectRequire) && !inventoryPlayer.GetInventoryFull())
            {
                inventoryPlayer.RemoveItem(objectRequire);
                inventoryPlayer.AddItem(objectContent);
                OnGiveObject?.Invoke();
                giveMyObject = true;
            }
        }
    }
}
