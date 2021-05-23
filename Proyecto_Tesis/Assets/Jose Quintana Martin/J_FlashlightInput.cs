using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_FlashlightInput : Updateable
{
    public Flashlight flashlight;

    private bool enableUseFlashlight = true;

    private bool isFlashligthOn = false;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateFlashlightInput);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    public void UpdateFlashlightInput()
    {
        if (enableUseFlashlight)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!isFlashligthOn)
                    flashlight.OnLight();
                else
                    flashlight.OffLight();

                isFlashligthOn = !isFlashligthOn;
            }
        }
    }

    public void SetEnableUseFlashlight(bool value) => enableUseFlashlight = value;

    public bool GetEnableUseFlashlight() { return enableUseFlashlight; }
}
