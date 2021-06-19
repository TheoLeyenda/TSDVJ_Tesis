using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private bool lockMouseInStart = true;
    private void Start()
    {
        SetCursorLockState(lockMouseInStart);
    }

    public void SetCursorLockState(bool activate)
    {
        if (activate)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
