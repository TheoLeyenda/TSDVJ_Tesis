using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_MapInteraction : Updateable
{
    public GameObject mapUI;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateMapButton);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    private void UpdateMapButton()
    {
        if (Input.GetButtonDown("OpenMap"))
            mapUI.SetActive(!mapUI.activeSelf);
    }
}
