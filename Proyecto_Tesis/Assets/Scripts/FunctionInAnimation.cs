using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FunctionInAnimation : MonoBehaviour
{
    [SerializeField] private UnityEvent functionInAnimation;

    public void EventInAnimation()
    {
        functionInAnimation?.Invoke();
    }
}
