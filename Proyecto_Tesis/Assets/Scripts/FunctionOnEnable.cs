using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionOnEnable : MonoBehaviour
{
    [SerializeField] private UnityEvent functionOnEnable;
    void OnEnable()
    {
        functionOnEnable?.Invoke();
    }
}
