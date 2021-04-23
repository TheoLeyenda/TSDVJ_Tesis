using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Updateable : MonoBehaviour
{
    protected UnityEvent MyUpdate;
    protected UnityEvent MyFixedUpdate;
    protected UpdateManager UM;

    protected virtual void Start()
    {
        MyUpdate = new UnityEvent();
        MyFixedUpdate = new UnityEvent();
        UM = UpdateManager.instanceUpdateManager;
    }

    protected void OnEnable()
    {
        if (MyUpdate != null)
            UM.UpdatesInGame.Add(MyUpdate);
    }

    protected void OnDisable()
    {
        if (MyUpdate != null)
            UM.UpdatesInGame.Remove(MyUpdate);
    }
}
