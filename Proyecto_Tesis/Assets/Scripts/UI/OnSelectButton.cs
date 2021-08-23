using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class OnSelectButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private UnityEvent OnSelectButtonFunction;

    public void OnSelect(BaseEventData eventData)
    {
        OnSelectButtonFunction?.Invoke();
    }
}
