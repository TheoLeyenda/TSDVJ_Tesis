using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_ObjectPlacer : J_Interactable
{
    public FunctionOnPlaceObjects fopo;
    public int myIndex;

    private void Start()
    {
#if UNITY_EDITOR
        if (fopo == null)
            Debug.LogError("Che, setea el FunctionOnPlaceObjects del objeto " + gameObject.name);
#endif
    }

    public override void Interact()
    {
        if (fopo.AddOrRemoveObject(myIndex))
        {
            base.Interact();
        }
    }
}
