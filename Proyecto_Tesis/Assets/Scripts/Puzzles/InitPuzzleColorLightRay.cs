using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPuzzleColorLightRay : MonoBehaviour
{
    [SerializeField] private List<ColorsToAssign> colors;
    [SerializeField] private List<SpritesToAssign> sprites;
    [SerializeField] private bool enableRepeatColors;
    [SerializeField] private bool useRandomIndexListenerColorLightRay = true;

    [SerializeField] private string nameItemAssignedSprites;

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
    }
    [SerializeField] private List<Data> dataToInit;

    private List<int> indexsToListenerColorLihtRay;

    private int currentSprite;
    private int currentColor;

    void Start()
    {
        indexsToListenerColorLihtRay = new List<int>();

        for (int i = 0; i < dataToInit.Count; i++)
        {
            indexsToListenerColorLihtRay.Add(i);
        }

        for (int i = 0; i < dataToInit.Count; i++)
        {
            if (useRandomIndexListenerColorLightRay)
            {

                int currentIndex = Random.Range(0, indexsToListenerColorLihtRay.Count);

                int index = indexsToListenerColorLihtRay[currentIndex];

                dataToInit[i].listenerColorLightRay.SetIndexListenerColorLightRay(index);

                //managerListenerColorLightRay.listenersColorRay[i] = dataToInit[index].listenerColorLightRay;

                indexsToListenerColorLihtRay.Remove(indexsToListenerColorLihtRay[currentIndex]);
            }

            if (dataToInit[i].item.useIconCompound)
            {
                for (int j = 0; j < dataToInit[i].item.iconsCompound.Length; j++)
                {
                    if (dataToInit[i].item.iconsCompound[j].name == "Dibujo")
                    {
                        currentColor = Random.Range(0, colors.Count);
                        currentSprite = Random.Range(0, sprites.Count);
                        if (enableRepeatColors)
                        {
                            dataToInit[i].item.iconsCompound[j].iconColor = colors[currentColor].color;
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
                            dataToInit[i].item.iconsCompound[j].iconColor = colors[currentColor].color;
                            dataToInit[i].listenerColorLightRay.SetAnswerColor(colors[currentColor].color);
                            dataToInit[i].spriteRendererColorCrayon.color = colors[currentColor].color;
                            colors[currentColor].assignedColor = true;
                        }

                        if (sprites[currentSprite].assignedSprite)
                        {
                            for (int k = 0; k < sprites.Count; k++)
                            {
                                if (!sprites[k].assignedSprite)
                                {
                                    currentSprite = k;
                                    k = colors.Count;
                                }
                            }
                        }

                        dataToInit[i].changeColorSprite.SetColorInArray(1, dataToInit[i].item.iconsCompound[j].iconColor);
                        if (!sprites[currentSprite].assignedSprite)
                        {
                            //Debug.Log(currentSprite);
                            //Debug.Log(sprites[currentSprite].sprite);

                            dataToInit[i].item.iconsCompound[j].iconSprite = sprites[currentSprite].sprite;
                            dataToInit[i].spriteRendererDibujo.sprite = sprites[currentSprite].sprite;
                            dataToInit[i].spriteRendererDibujoObjetivo.sprite = sprites[currentSprite].sprite;
                            sprites[currentSprite].assignedSprite = true;
                        }
                    }
                }
            }
        }
        managerListenerColorLightRay.InitManagerListenerColorLightRay();
    }
}