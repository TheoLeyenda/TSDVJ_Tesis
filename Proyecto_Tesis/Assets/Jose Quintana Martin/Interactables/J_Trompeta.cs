using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Trompeta : J_Interactable
{
    public ConditionForSpawnObjectInPosition itemspawner;

    private void Start()
    {
        itemspawner = GetComponent<ConditionForSpawnObjectInPosition>();
    }

    public override void Interact()
    {
        if (!enabled)
            return;

        if (itemspawner.CheckGiveMyObject())
        {
            base.Interact();
        }
        else
        {
            Debug.Log("Dialoguito");
        }
    }
}
