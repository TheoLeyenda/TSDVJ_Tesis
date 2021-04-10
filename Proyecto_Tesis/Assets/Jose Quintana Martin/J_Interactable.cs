using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class J_Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    public void DoAction()
    {
        action.Invoke();
    }
}
