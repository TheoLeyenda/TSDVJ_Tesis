using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    [SerializeField] private Sprite NewIcon;
    [SerializeField] private string NewDescriptionPhoto;
    [SerializeField] private int numberPhoto;
    public J_Item ItemPhoto;

    void OnEnable()
    {
        AccessCode.OnGenerateCode += AddNumberToText;
    }

    void OnDisable()
    {
        AccessCode.OnGenerateCode -= AddNumberToText;
    }

    public void AddNumberToText(int _numberPhoto, int number)
    {
        Debug.Log("OwO");
        if (numberPhoto == _numberPhoto)
        {
            Debug.Log("UwU");
            AddNewDescriptionPhoto(" " + number);
        }
    }

    public void SetNewDescriptionPhoto(string value) => NewDescriptionPhoto = value;

    public void AddNewDescriptionPhoto(string value) => NewDescriptionPhoto += value;

    public void SettingDataPhoto()
    {
        ItemPhoto.description = NewDescriptionPhoto;
        ItemPhoto.icon = NewIcon;
    }
}
