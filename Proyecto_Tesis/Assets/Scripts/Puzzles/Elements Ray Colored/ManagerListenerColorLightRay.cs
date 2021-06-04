using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class ManagerListenerColorLightRay : MonoBehaviour
{
    [SerializeField] private ListenerColorLightRay[] listenersColorRay;

    [SerializeField] private UnityEvent AllListenersCorrectAnswerFunction;

    [SerializeField] private bool iluminatyInOrderWindows = true;

    [SerializeField] private List<int> listenersInputsIndex;

    private int currentWindowIluminaty = 0;

    void Start()
    {
        listenersInputsIndex = new List<int>();
        for(int i = 0; i < listenersColorRay.Length; i++)
        {
            listenersColorRay[i].SetMyManagerListenerColorLightRay(this);
        }
    }

    public void DisbaleAllWindows()
    {
        for (int i = 0; i < listenersColorRay.Length; i++)
        {
            listenersColorRay[i].gameObject.layer = 0;
            listenersColorRay[i].enabled = false;
        }
    }

    public void CheckCorrectAnswersListeners(int currentWindowIndex)
    {
        if (currentWindowIndex > listenersColorRay.Length - 1 && currentWindowIndex < 0)
            return;

        bool doneAnswers = true;
        if (iluminatyInOrderWindows)
        {
            bool enableExaminate = true;

            for (int i = 0; i < listenersInputsIndex.Count; i++)
            {
                if (listenersInputsIndex[i] == currentWindowIndex && listenersColorRay[i].IsCorrectColor())
                {
                    enableExaminate = false;
                }
            }

            if (enableExaminate)
            {
                if (currentWindowIluminaty == currentWindowIndex && listenersColorRay[currentWindowIluminaty].IsCorrectColor())
                {
                    listenersInputsIndex.Add(currentWindowIndex);
                    currentWindowIluminaty++;
                }
                else
                {
                    listenersInputsIndex.Clear();
                    currentWindowIluminaty = 0;
                    for (int i = 0; i < listenersColorRay.Length; i++)
                    {
                        listenersColorRay[i].ResetWindows();
                    }
                    doneAnswers = false;
                }
            }
        }

        for (int i = 0; i < listenersColorRay.Length; i++)
        {
            if (!listenersColorRay[i].IsCorrectColor())
            {
                doneAnswers = false;
                i = listenersColorRay.Length;
            }
        }
        if (doneAnswers)
        {
            Debug.Log("Resolviste el puzzle puta");
            AllListenersCorrectAnswerFunction?.Invoke();
        }

    }
}
