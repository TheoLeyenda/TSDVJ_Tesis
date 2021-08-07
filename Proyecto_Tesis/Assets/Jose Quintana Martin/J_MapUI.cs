using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class J_MapUI : MonoBehaviour
{
    public Sprite lockedDoorIcon;
    public Sprite unlockedDoorIcon;
    public Sprite permalockedDoorIcon;
    public GameObject mapMarkersParent;

    public UnityEvent OnOpenMap;

    public UnityEvent OnCloseMap;

    public J_MapMarker[] markers;

    void OnEnable()
    {
        OnOpenMap?.Invoke();
    }

    void OnDisable()
    {
        OnCloseMap?.Invoke();
    }

    public void UpdateMap()
    {
        Debug.Log("Updating Map...");
        for (int i = 0; i < markers.Length; i++)
        {
            switch (markers[i].CheckMyDoorState())
            {
                case DoorState.Unlocked:
                    markers[i].ActivateSymbol();
                    markers[i].ChangeSymbol(unlockedDoorIcon);
                    break;
                case DoorState.Locked:
                    markers[i].ActivateSymbol();
                    markers[i].ChangeSymbol(lockedDoorIcon);
                    break;
                case DoorState.PermaLocked:
                    markers[i].ActivateSymbol();
                    markers[i].ChangeSymbol(permalockedDoorIcon);
                    break;
                case DoorState.Hidden:
                    break;
                default:
                    break;
            }
        }
    }
}
