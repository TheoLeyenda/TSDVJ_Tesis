using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    public enum TypePhoto
    {
        Horizontal,
        Vertical,
    }
    [SerializeField] private TypePhoto typePhoto;
    [SerializeField] private GameObject verticalImage;
    [SerializeField] private GameObject horizontalImage;
    [SerializeField] private Image imageVerticalPhoto;
    [SerializeField] private Image imageHorizontalPhoto;
    [SerializeField] private Sprite spriteVerticalPhoto;
    [SerializeField] private Sprite spriteHorizontalPhoto;
    [SerializeField] private TextMeshProUGUI titleTextHorizontal;
    [SerializeField] private TextMeshProUGUI descriptionTextHorizontal;
    [SerializeField] private TextMeshProUGUI titleTextVertical;
    [SerializeField] private TextMeshProUGUI descriptionTextVertical;
    [SerializeField] private J_Item item;
    [SerializeField] private bool useNameItem;
    [SerializeField] private bool useDescription;

    public void SettingDataPhoto()
    {
        switch (typePhoto)
        {
            case TypePhoto.Horizontal:
                verticalImage.SetActive(false);
                horizontalImage.SetActive(true);
                break;
            case TypePhoto.Vertical:
                verticalImage.SetActive(true);
                horizontalImage.SetActive(false);
                break;
        }

        imageVerticalPhoto.sprite = spriteVerticalPhoto;
        imageHorizontalPhoto.sprite = spriteHorizontalPhoto;

        if (useNameItem)
        {
            titleTextHorizontal.text = item.itemName;
            titleTextVertical.text = item.itemName;
        }
        else
        {
            titleTextHorizontal.text = "";
            titleTextVertical.text = "";
        }

        if (useDescription)
        {
            descriptionTextHorizontal.text = item.description;
            descriptionTextVertical.text = item.description;
        }
        else
        {
            descriptionTextHorizontal.text = "";
            descriptionTextVertical.text = "";
        }
    }
}
