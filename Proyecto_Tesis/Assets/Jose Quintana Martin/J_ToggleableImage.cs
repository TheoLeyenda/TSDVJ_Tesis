using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ToggleableImage : MonoBehaviour
{
    public Image image;
    private bool enableUse = true;
    public void ToggleImage() {
	if(enableUse)
        	image.gameObject.SetActive(!image.gameObject.activeSelf);
    }

    public void ShowImage() {
	if(enableUse)
        	image.gameObject.SetActive(true);
    }

    public void HideImage() {
	if(enableUse)
        	image.gameObject.SetActive(false);
    }
    public void SetEnableUse(bool value) => enableUse = value;
}
