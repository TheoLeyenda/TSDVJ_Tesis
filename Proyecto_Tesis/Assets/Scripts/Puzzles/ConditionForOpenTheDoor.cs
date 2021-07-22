using UnityEngine;
using UnityEngine.Events;

public class ConditionForOpenTheDoor : MonoBehaviour
{
    [SerializeField] private J_Item objectRequire;
    [SerializeField] private UnityEvent openDoorEvent;
    [SerializeField] private bool openDoorInGame = false;
    //private bool openDoor = false;
    public void CheckOpenDoor()
    {
        //if (!openDoor)
        //{
            if (J_inventoryManager.instance.HasItem(objectRequire) || openDoorInGame)
            {
                J_inventoryManager.instance.RemoveItem(objectRequire);
                openDoorEvent?.Invoke();
                //openDoor = true;
            }
        //}
    }
}
