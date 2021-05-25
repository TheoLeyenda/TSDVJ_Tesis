using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisableObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsActivate;
    [SerializeField] private List<GameObject> objectsDisable;

    [SerializeField] private bool useAutomaticDisable = true;

    public void UpdateObjects()
    {
        bool useDisable = true;

        if (useAutomaticDisable)
        {
            for (int i = 0; i < objectsActivate.Count; i++)
            {
                if (!objectsActivate[i].activeSelf)
                {
                    useDisable = false;
                }
            }
        }

        for (int i = 0; i < objectsDisable.Count; i++)
        {
            objectsDisable[i].SetActive(false);
        }

        if (!useDisable)
        {
            for (int i = 0; i < objectsActivate.Count; i++)
            {
                objectsActivate[i].SetActive(true);
            }
        }

        if (useAutomaticDisable && useDisable)
        {
            for (int i = 0; i < objectsActivate.Count; i++)
            {
                objectsActivate[i].SetActive(false);
            }
        }
    }
}
