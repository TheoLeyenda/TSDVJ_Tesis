using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flashlight : Updateable
{
    // Start is called before the first frame update

    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputOnAndOffFlashlight = "On And Off Flashlight";

    [SerializeField] private bool enableUseFlashlight = true;

    private bool isFlashligthOn = false;

    private float delayEnableUseFlashlight = 0.05f;

    public enum LightState
    {
        On,
        Off,
    }
    [SerializeField] private bool useFixedUpdateFlashlight = true;
    [SerializeField] private GameObject lightObject = null;
    [SerializeField] private LightState lightState = LightState.Off;
    [SerializeField] private FildOfView fildOfView = null;

    public void CheckOnViewFlash()
    {
        fildOfView.FindVisibleTargets();
    }

    protected override void Start()
    {
        base.Start();
        if (useFixedUpdateFlashlight)
        {
            MyFixedUpdate.AddListener(FixedUpdateFlashlight);
            UM.FixedUpdatesInGame.Add(MyFixedUpdate);
        }

        inputManager.GetInputFunction(nameInputOnAndOffFlashlight).myFunction = InputOnAndOffFlashlight;

    }

    public void FixedUpdateFlashlight()
    {
        switch (lightState)
        {
            case LightState.On:
                CheckOnViewFlash();
                if (!lightObject.activeSelf)
                    lightObject.SetActive(true);

                break;
            case LightState.Off:
                if (fildOfView.visibleTargets.Count > 0)
                    fildOfView.ClearVisibleTargets();

                if (lightObject.activeSelf)
                    lightObject.SetActive(false);
                break;
        }
    }

    public void OnLight()
    {
        lightState = LightState.On;
    }

    public void ChangeLight()
    {
        switch (lightState)
        {
            case LightState.On:
                lightState = LightState.Off;
                break;
            case LightState.Off:
                lightState = LightState.On;
                break;
        }
    }
    public void OffLight()
    {
        lightState = LightState.Off;
    }

    public void InputOnAndOffFlashlight()
    {
        if (enableUseFlashlight)
        {
            if (!isFlashligthOn)
                OnLight();
            else
                OffLight();

            isFlashligthOn = !isFlashligthOn;
        }
    }

    public void EnableUseFlashlightCoroutine()
    {
        //CORRUTINA PARA HABILITAR LA LUZ LUEGO DE LA PAUSA
        StartCoroutine("TimerForEnableUseFlashlight");
    }

    IEnumerator TimerForEnableUseFlashlight()
    {
        yield return new WaitForSeconds(delayEnableUseFlashlight);

        enableUseFlashlight = true;
    }

    public void SetEnableUseFlashlight(bool value) => enableUseFlashlight = value;

    public bool GetEnableUseFlashlight() { return enableUseFlashlight; }
}
