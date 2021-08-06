using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AddItemsToInventory))]
public class J_CadaverConserje : J_Interactable
{
    [HideInInspector]
    public AddItemsToInventory itemadd;
    bool interacted;

    private void Start()
    {
        itemadd = GetComponent<AddItemsToInventory>();
    }

    public override void Interact()
    {
        itemadd.AddItems(true);

        if (interacted)
        {
            Debug.Log("Dialogito bomnito!");
        }
        else
        {
            base.Interact();

            interacted = true;
        }
    }
}
