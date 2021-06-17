using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_SoundManager : MonoBehaviour
{
    public void PlayEvent(string eventPath, GameObject obj, string parameterName)
    {
        FMOD.Studio.EventInstance instance;
        instance = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        if (parameterName != "")
        {
            FMOD.Studio.PARAMETER_ID parameterID;

            FMOD.Studio.EventDescription eventDescription;
            instance.getDescription(out eventDescription);
            FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
            eventDescription.getParameterDescriptionByName(parameterName, out parameterDescription);
            parameterID = parameterDescription.id;

            instance.setParameterByID(parameterID, 1);
        }

        instance.start();
        instance.release();
    }
}
