using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPuzzleColorLightRay : MonoBehaviour
{
    [SerializeField] private List<ColorsToAssign> colors;
    [SerializeField] private List<SpritesToAssign> DrawSprite;
    [SerializeField] private List<Sprite> NumberSprite;
    private bool enableRepeatColors;
    [SerializeField] private string baseItemName = "Papel Con Dibujo";
    //[SerializeField] private bool useRandomIndexListenerColorLightRay = true;

    [SerializeField] private string nameItemAssignedSprites = "Dibujo";

    [SerializeField] private ManagerListenerColorLightRay managerListenerColorLightRay;

    [System.Serializable]
    public class SpritesToAssign
    {
        public Sprite sprite;
        [HideInInspector] public bool assignedSprite = false;
    }

    [System.Serializable]
    public class ColorsToAssign
    {
        public Color color;
        [HideInInspector] public bool assignedColor = false;
    }

    [System.Serializable]
    public class Data
    {
        public J_Item item;
        public ChangeColorSprite changeColorSprite;
        public ListenerColorLightRay listenerColorLightRay;
        public SpriteRenderer spriteRendererDibujoObjetivo; //Dibujo que esta en el pizzarron
        public SpriteRenderer spriteRendererColorCrayon; //color del machon de crayon que esta en el papel
        public SpriteRenderer spriteRendererDibujo; //Dibujo que esta en el papel
        public SpriteRenderer spriteRendererNumber;
        public Color colorCorrectAnswer;
    }
    [SerializeField] private List<Data> dataToInit;

    private List<int> indexsToListenerColorLihtRay;

    private int currentSprite;
    private int currentColor;

    void Start()
    {
        int porcentageEnableRepeatColors = Random.Range(0, 100);

        if (porcentageEnableRepeatColors > 50)
            enableRepeatColors = true;
        else
            enableRepeatColors = false;

        indexsToListenerColorLihtRay = new List<int>();

        for (int i = 0; i < dataToInit.Count; i++)
        {
            indexsToListenerColorLihtRay.Add(i);
        }

        for (int i = 0; i < dataToInit.Count; i++)
        {
            //if (useRandomIndexListenerColorLightRay)
            //{

                int currentIndex = Random.Range(0, indexsToListenerColorLihtRay.Count);

                int index = indexsToListenerColorLihtRay[currentIndex];

                dataToInit[i].listenerColorLightRay.SetIndexListenerColorLightRay(index);
                //managerListenerColorLightRay.listenersColorRay[i] = dataToInit[index].listenerColorLightRay;

                indexsToListenerColorLihtRay.Remove(indexsToListenerColorLihtRay[currentIndex]);
            //}



            if (dataToInit[i].item.useIconCompound)
            {
                for (int j = 0; j < dataToInit[i].item.iconsCompound.Length; j++)
                {
                    if (dataToInit[i].item.iconsCompound[j].name == nameItemAssignedSprites)
                    {
                        currentColor = Random.Range(0, colors.Count);
                        currentSprite = Random.Range(0, DrawSprite.Count);
                        if (enableRepeatColors)
                        {
                            dataToInit[i].colorCorrectAnswer = colors[currentColor].color;
                            dataToInit[i].item.iconsCompound[2].iconColor = colors[currentColor].color;
                            dataToInit[i].listenerColorLightRay.SetAnswerColor(colors[currentColor].color);
                            dataToInit[i].spriteRendererColorCrayon.color = colors[currentColor].color;
                            colors[currentColor].assignedColor = true;
                        }
                        else
                        {
                            if (colors[currentColor].assignedColor)
                            {
                                for (int k = 0; k < colors.Count; k++)
                                {
                                    if (!colors[k].assignedColor)
                                    {
                                        currentColor = k;
                                        k = colors.Count;
                                    }
                                }
                            }
                            dataToInit[i].colorCorrectAnswer = colors[currentColor].color;
                            dataToInit[i].item.iconsCompound[2].iconColor = colors[currentColor].color;
                            dataToInit[i].listenerColorLightRay.SetAnswerColor(colors[currentColor].color);
                            dataToInit[i].spriteRendererColorCrayon.color = colors[currentColor].color;
                            colors[currentColor].assignedColor = true;
                        }

                        if (DrawSprite[currentSprite].assignedSprite)
                        {
                            for (int k = 0; k < DrawSprite.Count; k++)
                            {
                                if (!DrawSprite[k].assignedSprite)
                                {
                                    currentSprite = k;
                                    k = colors.Count;
                                }
                            }
                        }

                        dataToInit[i].changeColorSprite.SetColorInArray(1, dataToInit[i].colorCorrectAnswer);

                        if (!DrawSprite[currentSprite].assignedSprite)
                        {
                            //Debug.Log(currentSprite);
                            //Debug.Log(sprites[currentSprite].sprite);

                            dataToInit[i].item.iconsCompound[j].iconSprite = DrawSprite[currentSprite].sprite;
                            dataToInit[i].spriteRendererDibujo.sprite = DrawSprite[currentSprite].sprite;
                            dataToInit[i].spriteRendererDibujoObjetivo.sprite = DrawSprite[currentSprite].sprite;
                            DrawSprite[currentSprite].assignedSprite = true;
                        }
                    }
                }
            }
        }

        managerListenerColorLightRay.InitManagerListenerColorLightRay();

        int numberSpriteIndex = 0;
        
        for (int j = 0; j < managerListenerColorLightRay.listenersInputsIndex.Count; j++)
        {
            for (int i = 0; i < dataToInit.Count; i++)
            {
                if (managerListenerColorLightRay.listenersInputsIndex[j] == i)
                {
                    dataToInit[i].spriteRendererNumber.sprite = NumberSprite[numberSpriteIndex];
                    dataToInit[i].item.iconsCompound[1].iconSprite = NumberSprite[numberSpriteIndex];
                    dataToInit[i].item.itemName = baseItemName + " " + (numberSpriteIndex + 1);
                    numberSpriteIndex++;
                    i = dataToInit.Count;
                }
            }
        }
    }
}