using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MOVERSE (Izquierda, derecha, adelante, atras) (HECHO)
//Interactuar (HECHO)
//Abrir el inventario (HECHO)
//Abrir el mapa (HECHO)
//Mover la camara (HECHO)
//Prender y apagar la linterna (HECHO)
//Cambiar los colores de la luz (HECHO)

public class InputManager : Updateable
{
    public enum TypeInput
    {
        Axis,
        Button,
    }
    public enum TypeInputPress
    {
        None,
        Button,
        ButtonDown,
        ButtonUp,
    }

    [SerializeField] private List<InputFunction> Inputs;
    private Dictionary<string, InputFunction> dictionaryInputFunctions;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateInputManager);
        UM.UpdatesInGame.Add(MyUpdate);

        dictionaryInputFunctions = new Dictionary<string, InputFunction>();
        for (int i = 0; i < Inputs.Count; i++)
        {
            dictionaryInputFunctions.Add(Inputs[i].GetName(), Inputs[i]);
        }
    }

    [System.Serializable]
    public class InputFunction
    {
        [SerializeField] private string name;
        public TypeInput typeInput;
        public TypeInputPress typeInputPress;
        public delegate void Function();
        public Function myFunction;

        public string GetName() { return name; }

        public void CheckInputFunction()
        {
            if (typeInput == TypeInput.Axis || name == "None")
                return;

            switch (typeInputPress)
            {
                case TypeInputPress.Button:
                    if (Input.GetButton(name))
                    {
                        myFunction?.Invoke();
                    }
                    break;
                case TypeInputPress.ButtonDown:
                    if (Input.GetButtonDown(name))
                    {
                        myFunction?.Invoke();
                    }
                    break;
                case TypeInputPress.ButtonUp:
                    if (Input.GetButtonUp(name))
                    {
                        myFunction?.Invoke();
                    }
                    break;
            }
        }

        public void GetAxisValue(ref float axis)
        {
            axis = Input.GetAxis(name);
        }

        public void GetAxisValueRaw(ref float axis)
        {
            axis = Input.GetAxisRaw(name);
        }
    }
    public InputFunction GetInputFunction(string name)
    {
        return dictionaryInputFunctions[name];
    }

    public void UpdateInputManager()
    {
        for (int i = 0; i < Inputs.Count; i++)
        {
            Inputs[i].CheckInputFunction();
        }
    }

}
