using UnityEngine;
using UnityEngine.Events;

public class ConditionForOpenTheDoor : MonoBehaviour
{
    [SerializeField] private J_Item objectRequire;
    [SerializeField] private J_Inventory inventoryPlayer;
    [SerializeField] private UnityEvent openDoorEvent;

    private bool openDoor = false;
    public void CheckOpenDoor()
    {
        if (!openDoor)
        {
            if (inventoryPlayer.PlayerHasItem(objectRequire.itemName))
            {
                inventoryPlayer.RemoveItem(objectRequire);
                openDoorEvent?.Invoke();
                openDoor = true;
            }
        }
    }
}
