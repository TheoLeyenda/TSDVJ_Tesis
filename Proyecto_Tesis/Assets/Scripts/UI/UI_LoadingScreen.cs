using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]
    public class LoadingScreen
    {
        public string adviceString;
        public Sprite backgroundSprite;
    }

    [SerializeField] private List<LoadingScreen> screens;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI adviceText;

    void Start()
    {
        GenerateBackground();
    }

    public void GenerateBackground()
    {
        int indexLoadingScreen = Random.Range(0, screens.Count);

        backgroundImage.sprite = screens[indexLoadingScreen].backgroundSprite;
        adviceText.text = screens[indexLoadingScreen].adviceString;
    }
    
}
