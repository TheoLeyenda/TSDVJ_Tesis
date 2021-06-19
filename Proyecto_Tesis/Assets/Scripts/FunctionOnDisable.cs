using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionOnDisable : MonoBehaviour
{
    [SerializeField] private UnityEvent functionOnDisable;
    void OnDisable()
    {
        functionOnDisable?.Invoke();
    }
}
