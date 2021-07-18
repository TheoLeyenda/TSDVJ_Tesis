using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckSendEventStune : Updateable
{
    // Start is called before the first frame update

    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputSendEventStune;
    [SerializeField] private Transform myUserTransform;
    [SerializeField] private float delayUseStuneEnable;
    private float auxDelayUseStuneEnable;
    private bool enableUse;
    public static event Action<CheckSendEventStune> OnSendEventStune;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateCheckEventStune);
        UM.UpdatesInGame.Add(MyUpdate);
        enableUse = true;

        inputManager.GetInputFunction(nameInputSendEventStune).myFunction = SendStune;
    }

    // Update is called once per frame
    public void UpdateCheckEventStune()
    {
        if(!enableUse)
        {
            if (delayUseStuneEnable > 0)
            {
                delayUseStuneEnable = delayUseStuneEnable - Time.deltaTime;
            }
            else
            {
                enableUse = true;
                delayUseStuneEnable = auxDelayUseStuneEnable;
            }
        }
    }

    public void SendStune()
    {
        if (enableUse)
        {
            if (OnSendEventStune != null)
                OnSendEventStune(this);
        }
    }

    public Transform GetMyUserTrasform() { return myUserTransform; }

    public void SetEnableUseSendEventStune(bool value) => enableUse = value;
}
