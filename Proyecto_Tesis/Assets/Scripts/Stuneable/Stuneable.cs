using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuneable : Updateable
{
    // Start is called before the first frame update
    protected bool inStune = false;
    [SerializeField] private float delayStune;

    public void CheckDelayStune(float delayStune, ref float speedObject, float auxSpeedObject)
    {
        if (delayStune > 0)
        {
            inStune = true;
            delayStune = delayStune - Time.deltaTime;
            speedObject = 0;
        }
        else
        {
            speedObject = auxSpeedObject;
            inStune = false;
        }
    }

    public void SetInStune(bool value) => inStune = value;

    public bool GetInStune() { return inStune; }

    public void SetDelayStune(float value) => delayStune = value; 

    public float GetDelayStune() { return delayStune; }
}
