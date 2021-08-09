using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_FuntionToItemLinker : MonoBehaviour
{
    public FunctionItem function;
    public J_Item item;

    bool enableSetFunctionItemInStart = true;

    private void Start()
    {
        if (enableSetFunctionItemInStart)
            LinkFunctionToItem();
    }

    public void LinkFunctionToItem()
    {
        item.SetFunctionItem(function);
    }
}
