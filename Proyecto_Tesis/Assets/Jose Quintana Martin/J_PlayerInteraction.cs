using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_PlayerInteraction : Updateable
{
    public GameObject playerCamera;
    public float interactionDistance = 1f;

    private J_Interactable interactable;

    RaycastHit hit;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateInteraction);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    public void UpdateInteraction()
    {
        if (Input.GetButtonDown("InteractTest"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            interactable = hit.transform.gameObject.GetComponent<J_Interactable>();
            if (interactable != null)
                interactable.DoAction();
            else
                Debug.Log("Not Ineractable");
        }
        else
            Debug.Log("No collider on range");
    }
}
