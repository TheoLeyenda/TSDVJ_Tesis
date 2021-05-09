using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Unlocked,
    Locked,
    PermaLocked,
    Hidden
}

public class J_Door : MonoBehaviour
{
    public J_Item myKey;
    public bool isPermalocked;
    public J_ItemChecker itemChecker;

    private DoorState doorState;
    private string keyItemName;

    private void Start()
    {
        doorState = DoorState.Hidden;
        if (myKey != null)
            keyItemName = myKey.itemName;
    }

    public DoorState GetDoorState()
    {
        return doorState;
    }

    public bool CheckPlayerHasMyKey()
    {
        if (itemChecker.checkPlayerHasItem(keyItemName))
        {
            doorState = DoorState.Unlocked;
            Debug.Log("La tiene");
            return true;
        }
        else
        {
            doorState = DoorState.Locked;
            Debug.Log("No la tiene");
            return false;
        }
    }

    public void DoorInteraction()
    {
        switch (doorState)
        {
            case DoorState.Unlocked:
                Debug.Log("OpenBehaviour");
                break;
            case DoorState.Locked:
                CheckPlayerHasMyKey();
                break;
            case DoorState.PermaLocked:
                Debug.Log("Permalocked :/");
                break;
            case DoorState.Hidden:
                if (!isPermalocked)
                    CheckPlayerHasMyKey();
                else
                    doorState = DoorState.PermaLocked;
                break;
            default:
                break;
        }
    }
}
