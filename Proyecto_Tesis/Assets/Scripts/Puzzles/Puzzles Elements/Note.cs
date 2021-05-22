using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Note : MonoBehaviour
{
    [SerializeField] private Image imageBackgroundNote;
    [SerializeField] private Sprite spriteBackgroundNote;
    [SerializeField] private J_Item itemNote;
    [SerializeField] private TextMeshProUGUI titleNote;
    [SerializeField] private TextMeshProUGUI textNote;

    public void SettingDataNote()
    {
        imageBackgroundNote.sprite = spriteBackgroundNote;
        titleNote.text = itemNote.itemName;
        textNote.text = itemNote.description;
    }


}
