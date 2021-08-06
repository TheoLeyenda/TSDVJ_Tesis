using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_CuadroJuanaDeArco : J_Interactable
{
    [HideInInspector]
    public HaveItemsInInventoryEvent e;

    private void Start()
    {
        e = GetComponent<HaveItemsInInventoryEvent>();
    }

    public override void Interact()
    {
        base.Interact();

        e.CheckHaveItemInInventoryEvent();
    }
}
