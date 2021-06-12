using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class ManagerListenerColorLightRay : MonoBehaviour
{
    public ListenerColorLightRay[] listenersColorRay;

    [SerializeField] private UnityEvent AllListenersCorrectAnswerFunction;

    [SerializeField] private bool iluminatyInOrderWindows = true;

    [SerializeField] private List<int> listenersInputsIndex = null;

    private int currentIndexListenersInputIndex = 0;

    private bool enableAddCurrentIndexListenersInputIndex = false;

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void Start()
    {
        InitManagerListenerColorLightRay();
    }
    public void InitManagerListenerColorLightRay()
    {
        if(listenersInputsIndex == null)
            listenersInputsIndex = new List<int>();

        listenersInputsIndex.Clear();
        for (int i = 0; i < listenersColorRay.Length; i++)
        {
            listenersInputsIndex.Add(listenersColorRay[i].GetIndexListenerColorLightRay());
            listenersColorRay[i].SetMyManagerListenerColorLightRay(this);
        }
    }
    public void DisbaleAllListenersColorRay()
    {
        for (int i = 0; i < listenersColorRay.Length; i++)
        {
            listenersColorRay[i].gameObject.layer = 0;
            listenersColorRay[i].enabled = false;
        }
    }

    private void CheckAddCurrentIndexListenersInputIndex(ColorLightRayEmitter colorLightRayEmitter)
    {

    }

    public void CheckCorrectAnswersListeners(ListenerColorLightRay _listenerColorLightRay)
    {
        if (_listenerColorLightRay.GetIndexListenerColorLightRay() > listenersColorRay.Length - 1 && _listenerColorLightRay.GetIndexListenerColorLightRay() < 0)
            return;

        bool doneAnswers = true;
        if (iluminatyInOrderWindows)
        {
            if (listenersInputsIndex[currentIndexListenersInputIndex] != _listenerColorLightRay.GetIndexListenerColorLightRay()
                && !listenersColorRay[currentIndexListenersInputIndex].IsCorrectColor())
            {
                //INCORRECTO
                for (int i = 0; i < listenersColorRay.Length; i++)
                {
                    listenersColorRay[i].ResetListenerColorRay();
                }
                currentIndexListenersInputIndex = 0;
            }
            else
            {
                enableAddCurrentIndexListenersInputIndex = true;
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
