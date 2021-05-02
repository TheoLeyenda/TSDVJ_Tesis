using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckSendEventStune : Updateable
{
    // Start is called before the first frame update

    public enum TypeCheckUse
    {
        Button,
        KeyCode,
    }

    [SerializeField] private TypeCheckUse typeCheckUse;
    [SerializeField] private string nameButtonCheck;
    [SerializeField] private KeyCode keyCodeCheck;
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
    }

    // Update is called once per frame
    public void UpdateCheckEventStune()
    {
        if (enableUse)
        {
            switch (typeCheckUse)
            {
                case TypeCheckUse.Button:
                    if (Input.GetButtonDown(nameButtonCheck))
                    {
                        if (OnSendEventStune != null)
                            OnSendEventStune(this);

                    }
                    break;
                case TypeCheckUse.KeyCode:
                    if (Input.GetKeyDown(keyCodeCheck))
                    {
                        //Debug.Log("Mande el stune");
                        if (OnSendEventStune != null)
                            OnSendEventStune(this);

                    }
                    break;
            }
        }
        else
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

    public Transform GetMyUserTrasform() { return myUserTransform; }

    public void SetEnableUseSendEventStune(bool value) => enableUse = value;
}
