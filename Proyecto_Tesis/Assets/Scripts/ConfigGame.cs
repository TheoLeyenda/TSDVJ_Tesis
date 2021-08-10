using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }
}
