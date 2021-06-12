using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class ManagerListenerColorLightRay : MonoBehaviour
{
    public ListenerColorLightRay[] listenersColorRay;

    [SerializeField] private UnityEvent AllListenersCorrectAnswerFunction;

    [SerializeField] private bool iluminatyInOrderListeners = true;

    [SerializeField] private List<int> listenersInputsIndex = null;

    private int currentIndexListenersInputIndex = 0;

    private bool enableAddCurrentIndexListenersInputIndex = false;

    private bool enableCheck = true;


    void OnEnable()
    {
        ColorLightRayEmitter.OnFailHit += EnableCheck;
    }

    void OnDisable()
    {
        ColorLightRayEmitter.OnFailHit -= EnableCheck;
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

    public void EnableCheck(ManagerListenerColorLightRay managerListenerColorLightRay)
    {
        if (managerListenerColorLightRay == this)
            enableCheck = true;
    }

    public void CheckCorrectAnswersListeners(ListenerColorLightRay _listenerColorLightRay)
    {
        bool doneAnswers = true;
        Debug.Log(enableCheck);
        if (iluminatyInOrderListeners && enableCheck)
        {
            //Debug.Log("currentIndexListenersInputIndex:" +currentIndexListenersInputIndex);
            if (listenersColorRay[listenersInputsIndex[0]] == _listenerColorLightRay
                && _listenerColorLightRay.IsCorrectColor())
            {
                //CORRECTO;
                Debug.Log("ENTRE 1");
                if (currentIndexListenersInputIndex == 0)
                {
                    enableAddCurrentIndexListenersInputIndex = true;
                }
                else
                {
                    enableAddCurrentIndexListenersInputIndex = false;
                    for (int i = 1; i < listenersInputsIndex.Count; i++)
                    {
                        listenersColorRay[listenersInputsIndex[i]].ResetListenerColorRay();
                    }
                    //currentIndexListenersInputIndex = 0;
                }
            }
            else if (listenersColorRay[listenersInputsIndex[currentIndexListenersInputIndex]] == _listenerColorLightRay
               && _listenerColorLightRay.IsCorrectColor())
            {
                Debug.Log("ENTRE 2");
                enableAddCurrentIndexListenersInputIndex = true;
            }
            else
            {
                Debug.Log("ENTRE 3");
                enableAddCurrentIndexListenersInputIndex = false;
                for (int i = 0; i < listenersColorRay.Length; i++)
                {
                    listenersColorRay[i].ResetListenerColorRay();
                }
                currentIndexListenersInputIndex = 0;
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

        if (enableAddCurrentIndexListenersInputIndex)
        {
            currentIndexListenersInputIndex++;
            enableAddCurrentIndexListenersInputIndex = false;
            enableCheck = false;
        }
    }
}
