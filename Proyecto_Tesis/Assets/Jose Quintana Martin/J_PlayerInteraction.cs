using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_PlayerInteraction : Updateable
{
    public Camera playerCamera;
    public float interactionDistance = 1f;
    public J_ToggleableImage interactImage;

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
        //Raycast
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            currIntObject = hit.transform.gameObject;
            //Debug.Log(hit.collider.name);
            if (currIntObject != prevIntObject)
            {
                interactable = currIntObject.GetComponent<J_Interactable>();
                prevIntObject = currIntObject;
            }
        }
        else
        {
            currIntObject = null;
            prevIntObject = null;
            interactable = null;
        }

        //Text
        if (interactable != null && interactable.enabled)
        {
            interactImage.ShowImage();
        }
        else
        {
            interactImage.HideImage();
        }

        //Input
        if (Input.GetButtonDown("InteractTest") && interactable.enabled)
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
