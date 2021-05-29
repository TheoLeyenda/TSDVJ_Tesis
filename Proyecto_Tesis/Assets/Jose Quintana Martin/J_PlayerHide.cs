using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_PlayerHide : Updateable
{
    public J_PlayerInteraction playerInteraction;
    public J_MouseInput mouseInput;
    public J_Movement movement;

    private bool isPlayerHidden;
    private Vector3 oldPos;

    public void SetHidden(bool _isplayerhidden)
    {
        isPlayerHidden = _isplayerhidden;
    }
    public bool GetHidden()
    {
        return isPlayerHidden;
    }

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateHide);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    public void UpdateHide()
    {
        if (isPlayerHidden && Input.GetButtonDown("InteractTest"))
            UnHide();
    }

    public void UnHide()
    {
        isPlayerHidden = false;
        mouseInput.enabled = true;
        movement.enabled = true;
        playerInteraction.enabled = true;

        transform.position = oldPos;
    }

    public void Hide()
    {
        mouseInput.enabled = false;
        movement.enabled = false;
        playerInteraction.enabled = false;
        isPlayerHidden = true;

        oldPos = transform.position;

        GameObject obj = playerInteraction.GetcurrIntObject();
        Vector3 newRot = obj.transform.eulerAngles;

        transform.position = obj.transform.position;
        transform.localRotation = Quaternion.Euler(newRot);
    }
}
