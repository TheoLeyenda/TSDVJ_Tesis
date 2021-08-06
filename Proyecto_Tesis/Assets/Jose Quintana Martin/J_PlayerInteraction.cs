using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class J_PlayerInteraction : Updateable
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputInteraction;

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

        inputManager.GetInputFunction(nameInputInteraction).myFunction = Interact;
    }

    public void UpdateInteraction()
    {
        ////Temporal, esto despues se mueve a otro script//
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    SceneManager.LoadScene("J_Menu");
        //}

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
        //if (Input.GetButtonDown("InteractTest"))
        //{
        //    Interact();
        //}
    }

    public void Interact()
    {
        if (interactable != null && interactable.enabled)
            interactable.Interact();
    }

    public GameObject GetcurrIntObject()
    {
        return currIntObject;
    }
}
