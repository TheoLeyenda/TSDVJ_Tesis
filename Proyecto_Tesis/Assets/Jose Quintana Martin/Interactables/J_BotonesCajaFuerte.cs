using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_BotonesCajaFuerte : J_Interactable
{
    public string myNumber;
    public AccessCode code;

    private void Start()
    {
        code = FindObjectOfType<AccessCode>();
    }

    public override void Interact()
    {
        if (!enabled)
            return;

        base.Interact();

        code.AddCaracterCode(myNumber);
    }
}
