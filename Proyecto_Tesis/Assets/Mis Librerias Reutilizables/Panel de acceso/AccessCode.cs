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

    [System.Serializable]
    public class RandomNumberCodeData
    {
        [SerializeField] private int minNumber;
        [SerializeField] private int maxNumber;

        private int codeNumber;

        [SerializeField] private DrawingSpriteForIndex drawingSpriteForIndex;

        public string GenerateNumberCode()
        {
            codeNumber = Random.Range(minNumber, maxNumber + 1);
            string code = codeNumber.ToString();

            for (int i = 0; i < code.ToCharArray().Length; i++)
            {
                string aux = ""+code.ToCharArray()[i];
                int index = int.Parse(aux);
                drawingSpriteForIndex.Draw(index);
                //Debug.Log(index);
            }

            return code;
        }

        public int GetCodeNumber()
        {
            return codeNumber;
        }
    }

    [SerializeField] private bool useRandomAnswerCode;
    [SerializeField] private List<RandomNumberCodeData> randomNumberCodeDatas;
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

    public static event System.Action<int, int> OnGenerateCode;

    private void Start()
    {
        if (useRandomAnswerCode)
        {
            if (randomNumberCodeDatas != null)
            {
                if (randomNumberCodeDatas.Count > 0)
                {
                    answerCode = "";
                    for (int i = 0; i < randomNumberCodeDatas.Count; i++)
                    {
                        answerCode += randomNumberCodeDatas[i].GenerateNumberCode();
                        if (OnGenerateCode != null)
                        {
                            OnGenerateCode(i + 1, randomNumberCodeDatas[i].GetCodeNumber());
                        }
                    }
                }
            }
        }

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
