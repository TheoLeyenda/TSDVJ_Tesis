using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ToggleableImage : MonoBehaviour
{
    public Image image;

    public void ToggleImage() {
        image.gameObject.SetActive(!image.gameObject.activeSelf);
    }

    public void ShowImage() {
        image.gameObject.SetActive(true);
    }

    public void HideImage() {
        image.gameObject.SetActive(false);
    }
}
