using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_FlashlightInput : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputOnAndOffFlashlight = "On And Off Flashlight";

    public Flashlight flashlight;

    private bool enableUseFlashlight = true;

    private bool isFlashligthOn = false;

    protected void Start()
    {
        inputManager.GetInputFunction(nameInputOnAndOffFlashlight).myFunction = InputOnAndOffFlashlight;
    }

    public void InputOnAndOffFlashlight()
    {
        if (enableUseFlashlight)
        {
            if (!isFlashligthOn)
                flashlight.OnLight();
            else
                flashlight.OffLight();

            isFlashligthOn = !isFlashligthOn;
        }
    }

    public void SetEnableUseFlashlight(bool value) => enableUseFlashlight = value;

    public bool GetEnableUseFlashlight() { return enableUseFlashlight; }
}
