using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_FlashlightInput : MonoBehaviour
{
    public Flashlight flashlight;

    private bool isFlashligthOn = false;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isFlashligthOn)
                flashlight.OnLight();
            else
                flashlight.OffLight();

            isFlashligthOn = !isFlashligthOn;
        }
    }
}
