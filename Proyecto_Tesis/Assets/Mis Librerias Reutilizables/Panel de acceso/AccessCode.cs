using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AccessCode : MonoBehaviour
{
    [System.Serializable]
    public class SpritesOutputPanel
    {
        public string nameSprite;
        public Sprite sprite;
    }

    [SerializeField] private string code;
    [SerializeField] private string answerCode;
    [SerializeField] private UnityEvent OnCodeDone;
    [SerializeField] private UnityEvent OnCodeFail;
    [SerializeField] private List<SpriteRenderer> OutputPanelSpriteRenderer;
    [SerializeField] private Color colorOutputPanel;
    [SerializeField] private List<SpritesOutputPanel> spritesOutputPanel;
    [SerializeField] private Sprite defaultSpriteButtonsPanel;

    [SerializeField] private bool useCheckOnFinishEnterCode = false;
    private int currentCountCharactersInCode = 0;
    private int countCharactersInCode;

    private int currentIndexPanel = 0;

    private void Start()
    {
        ClearCode();
    }

    public void ClearCode()
    {
        code = "";
        currentIndexPanel = 0;
        for(int i = 0; i < OutputPanelSpriteRenderer.Count; i++)
        {
            OutputPanelSpriteRenderer[i].sprite = defaultSpriteButtonsPanel;
            OutputPanelSpriteRenderer[i].color = colorOutputPanel;
        }
        currentCountCharactersInCode = 0;
        countCharactersInCode = OutputPanelSpriteRenderer.Count;
    }
    public void AddCaracterCode(string caracter)
    {
        if (currentIndexPanel < OutputPanelSpriteRenderer.Count)
        {
            code = code + caracter;
            OutputPanelSpriteRenderer[currentIndexPanel].sprite = GetSpriteOutputPanel(caracter);
            currentIndexPanel++;
        }

        if (useCheckOnFinishEnterCode)
        {
            currentCountCharactersInCode++;
            if (currentCountCharactersInCode >= countCharactersInCode)
                CheckCodeDone();
        }
    }

    private Sprite GetSpriteOutputPanel(string caracter)
    {
        for(int i = 0; i < spritesOutputPanel.Count; i++)
        {
            if (spritesOutputPanel[i].nameSprite == caracter)
                return spritesOutputPanel[i].sprite;
        }
        return null;
    }

    public void CheckCodeDone()
    {
        if(code == answerCode)
        {
            OnCodeDone?.Invoke();
        }
        else
        {
            //ClearCode();
            OnCodeFail?.Invoke();
        }
    } 
}
