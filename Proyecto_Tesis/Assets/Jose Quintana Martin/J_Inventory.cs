using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Inventory : MonoBehaviour
{
    [SerializeField] public List<J_Item> inventory;

    public void AddItem(J_Item item) {
        inventory.Add(item);
    }
}
