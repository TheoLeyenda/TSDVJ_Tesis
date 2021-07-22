using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Photo : J_ItemPickUp
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
        if (numberPhoto == _numberPhoto)
        {
            SetNewDescriptionPhoto("Foto del director a sus " + number + " años.");
        }
    }

    public void SetNewDescriptionPhoto(string value) => NewDescriptionPhoto = value;

    public void AddNewDescriptionPhoto(string value) => NewDescriptionPhoto += value;

    public void SettingDataPhoto()
    {
        ItemPhoto.description = NewDescriptionPhoto;
        ItemPhoto.icon = NewIcon;
    }

    public override void Interact()
    {
        base.Interact();

        SettingDataPhoto();
    }
}
