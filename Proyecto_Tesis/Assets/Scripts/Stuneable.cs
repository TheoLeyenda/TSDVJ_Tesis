using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuneable : MonoBehaviour
{
    // Start is called before the first frame update

    public void CheckDelayStune(float delayStune, ref float speedObject, float auxSpeedObject)
    {
        if (delayStune > 0)
        {
            delayStune = delayStune - Time.deltaTime;
            speedObject = 0;
        }
        else
        {
            speedObject = auxSpeedObject;
        }
    }
}
