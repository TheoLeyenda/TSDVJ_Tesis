using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_PlayerInteraction : Updateable
{
    public GameObject playerCamera;
    public float interactionDistance = 1f;
    public J_ScreenText screenText;

    private J_Interactable interactable;
    private GameObject currIntObject;
    private GameObject prevIntObject;

    RaycastHit hit;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateInteraction);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    public void UpdateInteraction()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            currIntObject = hit.transform.gameObject;
            if (currIntObject != prevIntObject)
            {
                interactable = currIntObject.GetComponent<J_Interactable>();
                prevIntObject = currIntObject;
            }
        }
        else
        {
            if (currIntObject != null)
            {
                prevIntObject = currIntObject;
                currIntObject = null;
            }
        }

        if (Input.GetButtonDown("InteractTest"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (interactable != null)
            interactable.DoAction();
    }

    public GameObject GetcurrIntObject()
    {
        return currIntObject;
    }
}
