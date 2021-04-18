using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputTestPlayer : Updateable
{
    [System.Serializable]
    public class InputAndEvent
    {
        public string InputName;
        public UnityEvent FunctionEvent;
        public TypeCheck typeCheck;

        public enum TypeCheck
        {
            GetButtonDown,
            GetButtonUp,
            GetButton,
        }


        public void CheckEvent()
        {
            switch (typeCheck)
            {
                case TypeCheck.GetButton:
                    if (Input.GetButton(InputName))
                    {
                        FunctionEvent?.Invoke();
                    }
                    break;
                case TypeCheck.GetButtonDown:
                    if (Input.GetButtonDown(InputName))
                    {
                        FunctionEvent?.Invoke();
                    }
                    break;
                case TypeCheck.GetButtonUp:
                    if (Input.GetButtonUp(InputName))
                    {
                        FunctionEvent?.Invoke();
                    }
                    break;
            }
        }
    }


    [SerializeField] private List<InputAndEvent> inputs = null;

    [SerializeField] private UnityEvent FunctionForNotInput = null;
    [SerializeField] private bool stopPlayerWithNullInput = true;
    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateInputTestPlayer);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    public void UpdateInputTestPlayer()
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            if (inputs[i] != null)
            {
                inputs[i].CheckEvent();
            }
        }

        if (stopPlayerWithNullInput && !Input.anyKey && !Input.anyKeyDown)
        {
            FunctionForNotInput?.Invoke();
        }
    }
}
