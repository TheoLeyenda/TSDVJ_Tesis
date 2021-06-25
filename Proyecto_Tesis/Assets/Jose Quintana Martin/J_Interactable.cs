using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class J_Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    [FMODUnity.EventRef]
    public string interactionEvent;

    [FMODUnity.ParamRef]
    public string paramRef;

    void Start() { }

    public void DoAction()
    {
        if(enabled)
            action.Invoke();

        //audio
        if (interactionEvent != "")
        {
            J_SoundManager _SoundManager = FindObjectOfType<J_SoundManager>();

            if (_SoundManager != null)
                _SoundManager.PlayEvent(interactionEvent, gameObject, paramRef);
        }
        //audio
    }
}
