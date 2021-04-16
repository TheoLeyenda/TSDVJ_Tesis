using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewFlashlight : MonoBehaviour
{
    public UnityEvent OnViewFlash;
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
        }
    }
}
