using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class ViewFlashlight : MonoBehaviour
{
    public UnityEvent OnViewFlash;
    public static event Action<ViewFlashlight> OnViewFlashAction;
    
    void OnEnable()
    {
        FildOfView.OnViewTarget += OnViewFlashFunction;
    }

    void OnDisable()
    {
        FildOfView.OnViewTarget -= OnViewFlashFunction;
    }

    void OnViewFlashFunction(Transform _transform)
    {
        if (transform == _transform)
        {
            OnViewFlash?.Invoke();

            if (OnViewFlashAction != null)
                OnViewFlashAction(this);
        }
    }

}
