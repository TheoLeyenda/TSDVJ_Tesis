using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_MapInteraction : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string nameInputOpenAndCloseMapUI;

    public GameObject mapUI;

    protected void Start()
    {
        inputManager.GetInputFunction(nameInputOpenAndCloseMapUI).myFunction = InputMapButton;
    }

    private void InputMapButton()
    {
        mapUI.SetActive(!mapUI.activeSelf);
    }
}
