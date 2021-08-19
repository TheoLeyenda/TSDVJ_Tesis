using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class J_Interactable : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string interactionEvent;

    [FMODUnity.ParamRef]
    public string paramRef;

    public UnityEvent OnInteractFunctions;

    public virtual void Interact()
    {
        PlaySound();

        OnInteractFunctions?.Invoke();
    }

    public void PlaySound()
    {
        if (interactionEvent != "")
        {
            J_SoundManager _SoundManager = FindObjectOfType<J_SoundManager>();

            if (_SoundManager != null)
                _SoundManager.PlayEvent(interactionEvent, gameObject, paramRef);
        }
    }
    public void PlaySound(string eventName)
    {
        if (eventName != "")
        {
            J_SoundManager _SoundManager = FindObjectOfType<J_SoundManager>();

            if (_SoundManager != null)
                _SoundManager.PlayEvent(eventName, gameObject, paramRef);
        }
    }
}
