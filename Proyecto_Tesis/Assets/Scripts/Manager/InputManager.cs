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
    private GameManager gm;
    [SerializeField] private List<InputFunction> Inputs; // Son afectados por la pausa.
    [SerializeField] private List<InputFunction> specialInputs; // No son afectados por la pausa.
    private Dictionary<string, InputFunction> dictionaryInputFunctions;

    void Awake()
    {
        dictionaryInputFunctions = new Dictionary<string, InputFunction>();
        for (int i = 0; i < Inputs.Count; i++)
        {
            dictionaryInputFunctions.Add(Inputs[i].GetName(), Inputs[i]);
        }
        for (int i = 0; i < specialInputs.Count; i++)
        {
            dictionaryInputFunctions.Add(specialInputs[i].GetName(), specialInputs[i]);
        }
    }

    protected override void Start()
    {
        gm = GameManager.instanceGameManager;
        base.Start();
        MyUpdate.AddListener(UpdateInputManager);
        UM.SpecialUpdatesInGame.Add(MyUpdate);
    }

    [System.Serializable]
    public class InputFunction
    {
        [SerializeField] private string name;
        public TypeInput typeInput;
        public TypeInputPress typeInputPress;
        public delegate void Function();
        public Function myFunction;

        private bool specialInput = false;

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
            if(specialInput || !GameManager.instanceGameManager.GetIsPauseGame())
                axis = Input.GetAxis(name);
        }

        public void GetAxisValueRaw(ref float axis)
        {
            if (specialInput || !GameManager.instanceGameManager.GetIsPauseGame())
                axis = Input.GetAxisRaw(name);
        }

        public void SetSpecialInput(bool value) => specialInput = value;

        public bool GetIsSpecialInput() { return specialInput; }
    }
    public InputFunction GetInputFunction(string name)
    {
        if (dictionaryInputFunctions.ContainsKey(name))
            return dictionaryInputFunctions[name];
        else
            return null;
    }

    public void UpdateInputManager()
    {

        for (int i = 0; i < specialInputs.Count; i++)
        {
            specialInputs[i].CheckInputFunction();
        }

        if (gm.GetIsPauseGame())
            return;

        for (int i = 0; i < Inputs.Count; i++)
        {
            Inputs[i].CheckInputFunction();
        }
    }

}
