using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FunctionByTimeExpired : Updateable
{
    public float timeEvent;
    private float auxTimeEvent;
    public UnityEvent OnTimeExpired;

    [SerializeField] private bool enableUseEvent = true;

    protected override void Start()
    {
        auxTimeEvent = timeEvent;
        base.Start();
        MyUpdate.AddListener(UpdateFunctionByTimeExpired);
        UM.SpecialUpdatesInGame.Add(MyUpdate);
    }

    void UpdateFunctionByTimeExpired()
    {
        if (enableUseEvent)
        {
            if (timeEvent > 0)
                timeEvent = timeEvent - Time.deltaTime;
            else
            {
                enableUseEvent = false;
                OnTimeExpired?.Invoke();
            }
        }
    }

    public void ResetTimeEvent()
    {
        enableUseEvent = true;
        timeEvent = auxTimeEvent;
    }

    public void SetNewTimeEvent(float value)
    {
        timeEvent = value;
        auxTimeEvent = value;
    }
}
