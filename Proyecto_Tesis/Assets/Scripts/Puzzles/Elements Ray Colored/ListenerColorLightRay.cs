using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerColorLightRay : MonoBehaviour
{
    bool isCorrectColor = false;
    [SerializeField] private bool lockWindowsToCorrectAnswer = true;

    private ManagerListenerColorLightRay myManagerListenerColorLightRay;

    [SerializeField] private UnityEvent OnCorrectAnswer;
    [SerializeField] private UnityEvent OnIncorrectAnswer;

    [SerializeField] private Color answerColor;

    [SerializeField] private int indexListenerColorLightRay;

    public void SetIndexListenerColorLightRay(int value) => indexListenerColorLightRay = value;

    public int GetIndexListenerColorLightRay() { return indexListenerColorLightRay; }

    public void SetAnswerColor(Color color) => answerColor = color;

    public bool IsCorrectColor(){return isCorrectColor; }

    public void SetIsCorrectColor(bool value) => isCorrectColor = value;

    public void ResetListenerColorRay()
    {
        OnIncorrectAnswer?.Invoke();
        isCorrectColor = false;
    }

    public void CheckIsCorrectColor(Color color)
    {
        Vector3 vecAnswerColor = new Vector3(answerColor.r, answerColor.g, answerColor.b);
        Vector3 vecColor = new Vector3(color.r, color.g, color.b);

        bool rAnswer = answerColor.r == color.r;
        bool gAnswer = answerColor.g == color.g;
        bool bAnswer = answerColor.b == color.b;

        if (rAnswer && gAnswer && bAnswer)
        {
            //Debug.Log("Correct Answer");
            OnCorrectAnswer?.Invoke();
            isCorrectColor = true;
        }
        else if(!lockWindowsToCorrectAnswer)
        {
            //Debug.Log("Incorrect Answer");
            OnIncorrectAnswer?.Invoke();
            isCorrectColor = false;
        }
        myManagerListenerColorLightRay.CheckCorrectAnswersListeners(this);
    }

    public void SetMyManagerListenerColorLightRay(ManagerListenerColorLightRay managerListenerColorLightRay) => myManagerListenerColorLightRay = managerListenerColorLightRay;
}
