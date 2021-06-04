using UnityEngine;
using UnityEngine.Events;

public class ManagerListenerColorLightRay : MonoBehaviour
{
    [SerializeField] private ListenerColorLightRay[] listenersColorRay;

    [SerializeField] private UnityEvent AllListenersCorrectAnswerFunction;

    void Start()
    {
        for(int i = 0; i < listenersColorRay.Length; i++)
        {
            listenersColorRay[i].SetMyManagerListenerColorLightRay(this);
        }
    }

    public void CheckCorrectAnswersListeners()
    {
        bool doneAnswers = true;
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
