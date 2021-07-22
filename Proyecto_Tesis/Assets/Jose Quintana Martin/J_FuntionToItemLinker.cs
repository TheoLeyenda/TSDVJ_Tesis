using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_FuntionToItemLinker : MonoBehaviour
{
    public FunctionItem function;
    public J_Item item;

    private void Start()
    {
        item.SetFunctionItem(function);
    }
}
