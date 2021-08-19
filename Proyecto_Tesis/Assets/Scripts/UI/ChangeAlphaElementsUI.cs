using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeAlphaElementsUI : Updateable
{
    private bool enableChangeAlpha0 = false;
    private bool enableChangeAlpha1 = false;

    [SerializeField] private List<MaskableGraphic> elementsUI_MaskableGraphic;
    [SerializeField] private List<Selectable> elementsUI_Selectable;
    [SerializeField] private float speedChangeAlpha;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateChangeAlphaImages);
        UM.SpecialUpdatesInGame.Add(MyUpdate);
    }

    void UpdateChangeAlphaImages()
    {
        if (!enableChangeAlpha0 && !enableChangeAlpha1)
            return;

        if (enableChangeAlpha0)
        {
            float a = 0;
            for (int i = 0; i < elementsUI_MaskableGraphic.Count; i++)
            {
                if (elementsUI_MaskableGraphic[i].color.a > 0)
                {
                    a = elementsUI_MaskableGraphic[i].color.a - (Time.deltaTime * speedChangeAlpha);
                    elementsUI_MaskableGraphic[i].color = new Color(elementsUI_MaskableGraphic[i].color.r, elementsUI_MaskableGraphic[i].color.g, elementsUI_MaskableGraphic[i].color.b, a);
                }
            }

            a = 0;
            for (int i = 0; i < elementsUI_Selectable.Count; i++)
            {
                ColorBlock colorBlock = elementsUI_Selectable[i].colors;
                if (elementsUI_Selectable[i].colors.normalColor.a > 0)
                {
                    a = colorBlock.normalColor.a - (Time.deltaTime * speedChangeAlpha);
                    colorBlock.normalColor = new Color(colorBlock.normalColor.r, colorBlock.normalColor.g, colorBlock.normalColor.b, a);
                }

                if (elementsUI_Selectable[i].colors.highlightedColor.a > 0)
                {
                    a = colorBlock.highlightedColor.a - (Time.deltaTime * speedChangeAlpha);
                    colorBlock.highlightedColor = new Color(colorBlock.highlightedColor.r, colorBlock.highlightedColor.g, colorBlock.highlightedColor.b, a);
                }

                if (elementsUI_Selectable[i].colors.pressedColor.a > 0)
                {
                    a = colorBlock.pressedColor.a - (Time.deltaTime * speedChangeAlpha);
                    colorBlock.pressedColor = new Color(colorBlock.pressedColor.r, colorBlock.pressedColor.g, colorBlock.pressedColor.b, a);
                }

                if (elementsUI_Selectable[i].colors.selectedColor.a > 0)
                {
                    a = colorBlock.selectedColor.a - (Time.deltaTime * speedChangeAlpha);
                    colorBlock.selectedColor = new Color(colorBlock.selectedColor.r, colorBlock.selectedColor.g, colorBlock.selectedColor.b, a);
                }
                elementsUI_Selectable[i].colors = colorBlock;
            }

            bool doneChange = true;
            for (int i = 0; i < elementsUI_MaskableGraphic.Count; i++)
            {
                if (elementsUI_MaskableGraphic[i].color.a > 0)
                {
                    doneChange = false;
                    i = elementsUI_MaskableGraphic.Count;
                }
            }

            for (int i = 0; i < elementsUI_Selectable.Count; i++)
            {
                if (elementsUI_Selectable[i].colors.normalColor.a > 0 
                    || elementsUI_Selectable[i].colors.highlightedColor.a > 0
                    || elementsUI_Selectable[i].colors.pressedColor.a > 0
                    || elementsUI_Selectable[i].colors.selectedColor.a > 0)
                {
                    doneChange = false;
                    i = elementsUI_Selectable.Count;
                }
            }

            if (doneChange)
                enableChangeAlpha0 = false;
        }
        else
        {
            float a = 0;
            for (int i = 0; i < elementsUI_MaskableGraphic.Count; i++)
            {
                if (elementsUI_MaskableGraphic[i].color.a < 1)
                {
                    a = elementsUI_MaskableGraphic[i].color.a + (Time.deltaTime * speedChangeAlpha);
                    elementsUI_MaskableGraphic[i].color = new Color(elementsUI_MaskableGraphic[i].color.r, elementsUI_MaskableGraphic[i].color.g, elementsUI_MaskableGraphic[i].color.b, a);
                }
            }

            a = 0;
            for (int i = 0; i < elementsUI_Selectable.Count; i++)
            {
                ColorBlock colorBlock = elementsUI_Selectable[i].colors;
                if (elementsUI_Selectable[i].colors.normalColor.a < 1)
                {
                    a = colorBlock.normalColor.a + (Time.deltaTime * speedChangeAlpha);
                    colorBlock.normalColor = new Color(colorBlock.normalColor.r, colorBlock.normalColor.g, colorBlock.normalColor.b, a);
                }

                if (elementsUI_Selectable[i].colors.highlightedColor.a < 1)
                {
                    a = colorBlock.highlightedColor.a + (Time.deltaTime * speedChangeAlpha);
                    colorBlock.highlightedColor = new Color(colorBlock.highlightedColor.r, colorBlock.highlightedColor.g, colorBlock.highlightedColor.b, a);
                }

                if (elementsUI_Selectable[i].colors.pressedColor.a < 1)
                {
                    a = colorBlock.pressedColor.a + (Time.deltaTime * speedChangeAlpha);
                    colorBlock.pressedColor = new Color(colorBlock.pressedColor.r, colorBlock.pressedColor.g, colorBlock.pressedColor.b, a);
                }

                if (elementsUI_Selectable[i].colors.selectedColor.a < 1)
                {
                    a = colorBlock.selectedColor.a + (Time.deltaTime * speedChangeAlpha);
                    colorBlock.selectedColor = new Color(colorBlock.selectedColor.r, colorBlock.selectedColor.g, colorBlock.selectedColor.b, a);
                }
                elementsUI_Selectable[i].colors = colorBlock;
            }

            bool doneChange = true;
            for (int i = 0; i < elementsUI_MaskableGraphic.Count; i++)
            {
                if (elementsUI_MaskableGraphic[i].color.a < 1)
                {
                    doneChange = false;
                    i = elementsUI_MaskableGraphic.Count;
                }
            }

            for (int i = 0; i < elementsUI_Selectable.Count; i++)
            {
                if (elementsUI_Selectable[i].colors.normalColor.a < 1
                    || elementsUI_Selectable[i].colors.highlightedColor.a < 1
                    || elementsUI_Selectable[i].colors.pressedColor.a < 1
                    || elementsUI_Selectable[i].colors.selectedColor.a < 1)
                {
                    doneChange = false;
                    i = elementsUI_Selectable.Count;
                }
            }

            if (doneChange)
                enableChangeAlpha1 = false;
        }
    }

    public void SetSpeedChangeAlpha(float value)
    {
        speedChangeAlpha = value;
    }

    public void SetAlphaObjects(float value)
    {
        for (int i = 0; i < elementsUI_MaskableGraphic.Count; i++)
        {
            elementsUI_MaskableGraphic[i].color = new Color(elementsUI_MaskableGraphic[i].color.r
                , elementsUI_MaskableGraphic[i].color.g
                , elementsUI_MaskableGraphic[i].color.b
                , value);
        }

        for (int i = 0; i < elementsUI_Selectable.Count; i++)
        {
            ColorBlock colorBlock = elementsUI_Selectable[i].colors;
            colorBlock.normalColor = new Color(colorBlock.normalColor.r
                , colorBlock.normalColor.g
                , colorBlock.normalColor.b
                , value);
            colorBlock.highlightedColor = new Color(colorBlock.highlightedColor.r
                , colorBlock.highlightedColor.g
                , colorBlock.highlightedColor.b
                , value);
            colorBlock.pressedColor = new Color(colorBlock.pressedColor.r
                , colorBlock.pressedColor.g
                , colorBlock.pressedColor.b
                , value);

            colorBlock.selectedColor = new Color(colorBlock.selectedColor.r
                , colorBlock.selectedColor.g
                , colorBlock.selectedColor.b
                , value);
            
            elementsUI_Selectable[i].colors = colorBlock;
        }
    }

    public void ChageAlpha1()
    {
        enableChangeAlpha0 = false;
        enableChangeAlpha1 = true;
    }

    public void ChangeAlpha0()
    {
        enableChangeAlpha0 = true;
        enableChangeAlpha1 = false;
    }

}
