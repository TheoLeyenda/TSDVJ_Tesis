using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_MapMarker : MonoBehaviour
{
    public J_Door myDoor;

    public Image mySymbol;

    private void Start()
    {
        mySymbol = GetComponent<Image>();
    }

    public void ChangeSymbol(Sprite newSymbol)
    {
        mySymbol.sprite = newSymbol;
    }

    public void ActivateSymbol()
    {
        mySymbol.gameObject.SetActive(true);
    }

    public DoorState CheckMyDoorState()
    {
        return myDoor.GetDoorState();
    }
}
