using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DoorState
{
    Unlocked,
    Locked,
    PermaLocked,
    Hidden
}

public class J_Door : J_Interactable
{
    [FMODUnity.EventRef]
    public string doorOpenEvent;

    [FMODUnity.EventRef]
    public string doorClosedEvent;

    public J_Item myKey;
    public bool isPermalocked;
    public J_Inventory playerInventoryRef;

    [SerializeField] private DoorState doorState = DoorState.Hidden;
    [SerializeField] private UnityEvent openAction;
    [SerializeField] private UnityEvent lockedAction;
    [SerializeField] private UnityEvent permalockedAction;


    private void Start()
    {
        playerInventoryRef = FindObjectOfType<J_Inventory>();
    }

    public DoorState GetDoorState()
    {
        return doorState;
    }

    public bool CheckPlayerHasMyKey()
    {
        if (playerInventoryRef.PlayerHasItem(myKey) || myKey == null)
        {
            doorState = DoorState.Unlocked;
            Debug.Log("La tiene");
            DoorInteraction();
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
                openAction.Invoke();
                PlaySound(doorOpenEvent);
                break;
            case DoorState.Locked:
                CheckPlayerHasMyKey();
                lockedAction.Invoke();
                PlaySound(doorClosedEvent);
                break;
            case DoorState.PermaLocked:
                permalockedAction.Invoke();
                PlaySound(doorClosedEvent);
                break;
            case DoorState.Hidden:
                if (!isPermalocked)
                    CheckPlayerHasMyKey();
                else
                    doorState = DoorState.PermaLocked;
                DoorInteraction();
                break;
            default:
                break;
        }
    }

    public override void Interact()
    {
        base.Interact();

        DoorInteraction();
    }
}
