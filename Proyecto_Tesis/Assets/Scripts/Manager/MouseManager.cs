using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private bool lockMouseInStart = true;
    private bool lockCursor = true;
    private void Start()
    {
        SetCursorLockState(lockMouseInStart);
        lockCursor = lockMouseInStart;
    }

    public void SetCursorLockState(bool activate)
    {
        if (activate)
        {
            //Debug.Log("DESPAUSA D:");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            lockCursor = true;
        }
        else
        {
            //Debug.Log("PAUSA :D");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            lockCursor = false;
        }
    }

    public bool GetLockCursor() { return lockCursor; }
}
