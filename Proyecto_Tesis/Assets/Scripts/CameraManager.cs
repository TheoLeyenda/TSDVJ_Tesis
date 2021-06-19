using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<Camera> camerasInGame;
    public void DisableAllCameras()
    {
        for (int i = 0; i < camerasInGame.Count; i++)
        {
            camerasInGame[i].gameObject.SetActive(false);
        }
    }
    public void SwitchCamera(int index)
    {
        if (index < 0 || index >= camerasInGame.Count)
            return;

        DisableAllCameras();

        camerasInGame[index].gameObject.SetActive(true);

    }
    public void OnCameraForIndex(int index)
    {
        if (index < 0 || index >= camerasInGame.Count)
            return;

        camerasInGame[index].gameObject.SetActive(true);

    }
    public void OffCameraForIndex(int index)
    {
        if (index < 0 || index >= camerasInGame.Count)
            return;

        camerasInGame[index].gameObject.SetActive(false);
    }

}
