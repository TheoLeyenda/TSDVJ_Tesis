using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AccesCodeButton : MonoBehaviour
{
    public UnityEvent OnInteractiveButton;

    public void InteractiveButtonEvent()
    {
        OnInteractiveButton?.Invoke();
    }
}
