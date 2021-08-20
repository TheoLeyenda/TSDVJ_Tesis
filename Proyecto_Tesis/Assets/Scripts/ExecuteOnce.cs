using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ExecuteOnce : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool exectuteStart = false;
    public UnityEvent FunctionExecute;

    private bool functionExecuteDone = false;

    void Start()
    {
        if (exectuteStart)
            FunctionExecute?.Invoke();
    }

    public void ExecuteFunction()
    {
        if (!functionExecuteDone)
        {
            FunctionExecute?.Invoke();
            functionExecuteDone = true;
        }
    }
}
