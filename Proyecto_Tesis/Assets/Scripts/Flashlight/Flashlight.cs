using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flashlight : Updateable
{
    // Start is called before the first frame update
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
}
