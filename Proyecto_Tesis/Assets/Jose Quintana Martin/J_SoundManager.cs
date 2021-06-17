using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_SoundManager : MonoBehaviour
{
    public void PlayEvent(string eventPath, GameObject obj)
    {
        FMOD.Studio.EventInstance instance;
        instance = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        instance.start();
        instance.release();
    }
}
