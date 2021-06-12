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

    [Header("----------------------")]
    public GameObject parent;
    public Image imageGo;
    private List<GameObject> gameObjects;
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

    public void UpdateImage(J_Item.IconCompound[] icons)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            gameObjects.Add(Instantiate(imageGo.gameObject, icons[i].position, Quaternion.identity, parent.transform));
            gameObjects[gameObjects.Count - 1].transform.eulerAngles = icons[i].eulerRotation;
            gameObjects[gameObjects.Count - 1].transform.localScale = icons[i].scale;
            Image image = gameObjects[gameObjects.Count - 1].GetComponent<Image>();

            if (image != null)
            {
                image.sprite = icons[i].iconSprite;
                image.color = icons[i].iconColor;
            }
        }
    }

    public void DestroyImagesIcons()
    {
        for (int i = gameObjects.Count - 1; i >= 0 ; i--)
        {
            Destroy(gameObjects[i]);
        }
        gameObjects.Clear();
    }
}
