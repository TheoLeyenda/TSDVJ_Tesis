using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class J_Item : ScriptableObject
{
    public string itemName = "New Item";
    public string description = "Description";
    public Sprite icon = null;
    private FunctionItem functionItem;

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
}
