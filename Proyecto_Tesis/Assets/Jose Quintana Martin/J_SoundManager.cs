using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_SoundManager : MonoBehaviour
{
    public void PlayEvent(string eventPath)
    {
        FMOD.Studio.EventInstance instance;
        instance = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        instance.start();
        instance.release();
    }
}
