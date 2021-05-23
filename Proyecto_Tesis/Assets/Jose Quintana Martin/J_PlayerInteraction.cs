using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_PlayerInteraction : Updateable
{
    public GameObject playerCamera;
    public float interactionDistance = 1f;
    public LayerMask interactionMask;
    public J_ScreenText screenText;

    private J_Interactable interactable;
    private GameObject lastIntObject;

    RaycastHit hit;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateInteraction);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    public void UpdateInteraction()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactionMask))
        {
            if (interactable == null)
            {
                Debug.Log("changos");
                interactable = hit.transform.gameObject.GetComponent<J_Interactable>();
                lastIntObject = interactable.gameObject;

                if (screenText != null)
                {
                    screenText.ToggleText();
                    screenText.SetText("Press E to interact");
                }
            }
        }
        else
        {
            interactable = null;
            screenText.HideText();
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

    public GameObject GetLastIntObject()
    {
        return lastIntObject;
    }
}
