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

    // Start is called before the first frame update
    [SerializeField] private InputAndEvent LeftMovement;
    [SerializeField] private InputAndEvent RightMovement;
    [SerializeField] private InputAndEvent ForwardMovement;
    [SerializeField] private InputAndEvent BackMovement;
    [SerializeField] private InputAndEvent Jump;
    [SerializeField] private UnityEvent FunctionForNotInput;
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
        LeftMovement.CheckEvent();
        RightMovement.CheckEvent();
        ForwardMovement.CheckEvent();
        BackMovement.CheckEvent();
        Jump.CheckEvent();

        if (stopPlayerWithNullInput && !Input.anyKey && !Input.anyKeyDown)
        {
            FunctionForNotInput?.Invoke();
        }
    }
}
