using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class J_Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
}
