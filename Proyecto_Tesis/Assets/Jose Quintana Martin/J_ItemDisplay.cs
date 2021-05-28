using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class J_ItemDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Image itemImage;

    public void UpdateName(string newName)
    {
        itemName.text = newName;
    }

    public void UpdateDescription(string newDescription)
    {
        itemDescription.text = newDescription;
    }

    public void UpdateImage(Sprite newImage)
    {
        itemImage.sprite = newImage;
    }
}
