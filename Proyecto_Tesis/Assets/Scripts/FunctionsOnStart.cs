using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FunctionsOnStart : MonoBehaviour
{
    [SerializeField] private UnityEvent functionOnStart;

    void Start()
    {
        functionOnStart?.Invoke();
    }
}
