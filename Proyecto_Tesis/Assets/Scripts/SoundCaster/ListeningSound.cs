using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class ListeningSound : MonoBehaviour
{
    public UnityEvent OnListeningSound;

    public float volumeSound { set; get; }

    public static event Action<ListeningSound, float> OnListeningSoundAction;

    public void ListeningSoundAction()
    {
        if (OnListeningSoundAction != null)
            OnListeningSoundAction(this, volumeSound);
    }
}
