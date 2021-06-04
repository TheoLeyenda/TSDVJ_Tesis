using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorSprite : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void ChangeColor(int indexColor)
    {
        Debug.Log("Cambie color");
        if (indexColor < colors.Length && indexColor >= 0)
        {
            Debug.Log("Si padre");
            spriteRenderer.color = colors[indexColor];
        }
    }
}
