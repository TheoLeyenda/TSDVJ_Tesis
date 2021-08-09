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
    public Sprite defaultSprite;

    [Header("----------------------")]
    public GameObject parent;
    public Image imageGo;
    [SerializeField] private List<Image> imagesIconCompound;

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
        Debug.Log(gameObject.name);
        Debug.Log(imagesIconCompound.Count);
        for (int i = 0; i < icons.Length; i++)
        {
            Debug.Log("SABES QUE SI");
            imagesIconCompound[i].sprite = icons[i].iconSprite;
            imagesIconCompound[i].color = icons[i].iconColor;
        }
    }

    public void DestroyImagesIcons()
    {
        for (int i = 0; i < imagesIconCompound.Count; i++)
        {
            imagesIconCompound[i].sprite = defaultSprite;
            imagesIconCompound[i].color = Color.white;
        }
    }
}
