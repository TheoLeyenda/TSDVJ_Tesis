using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class J_Item : ScriptableObject
{
    public string itemName = "New Item";
    public string description = "Description";
    public Sprite icon = null;
    private FunctionItem functionItem;

    public bool useIconCompound = false;

    public IconCompound[] iconsCompound;

    [System.Serializable]
    public class IconCompound
    {
        public string name;
        public Color iconColor;
        public Sprite iconSprite;
    }

    public void SetDescription(string value) => description = value;

    public void SetFunctionItem(FunctionItem _functionItem)
    {
        functionItem = _functionItem;
    }

    public FunctionItem GetFunctionItem()
    {
        return functionItem;
    }

    public void InvokeFunctionItem()
    {
        if(functionItem != null)
            functionItem.functionItem?.Invoke();
    }

    public void SetIconColor(string name, Color color)
    {
        for (int i = 0; i < iconsCompound.Length; i++)
        {
            iconsCompound[i].iconColor = color;
        }
    }
    public void SetIconSprite(string name, Sprite sprite)
    {
        for (int i = 0; i < iconsCompound.Length; i++)
        {
            iconsCompound[i].iconSprite = sprite;
        }
    }
}
