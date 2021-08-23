using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ObjetivesInGameManager : MonoBehaviour
{
    [System.Serializable]
    public class Objetive
    {
        private bool descriptionObjectiveSettingDone = false;
        private string descriptionObjetive;
        public bool useCountObjects;
        public int currentCountObjects;
        public int objetiveCountObjects;
        public bool objetiveComplete;
        public TextMeshProUGUI textObjetive;
        public UnityEvent OnObjetiveDone;
        public void UpdateDrawObjetive()
        {
            if (!descriptionObjectiveSettingDone)
            {
                descriptionObjetive = textObjetive.text;
                descriptionObjectiveSettingDone = true;
            }
            if (!textObjetive.gameObject.activeSelf)
                ActiveText();

            if (useCountObjects)
            {
                textObjetive.text = descriptionObjetive;
                textObjetive.text = textObjetive.text + " (" + currentCountObjects + "/" + objetiveCountObjects + ").";
            }
            if (objetiveComplete)
            {
                textObjetive.fontStyle = FontStyles.Strikethrough;
                OnObjetiveDone?.Invoke();
            }
            //else
            //textObjetive.fontStyle = FontStyles.Normal;
        }

        public void AddCurrentCountObjectToObjetive()
        {
            currentCountObjects++;
        }

        public void CheckCompleteObjetiveForTakeObjects()
        {
            if (useCountObjects)
            {
                if (currentCountObjects >= objetiveCountObjects)
                    objetiveComplete = true;
            }
        }

        public void SetObjectiveComplete(bool value) => objetiveComplete = value;

        public void ActiveText()
        {
            textObjetive.gameObject.SetActive(true);
        }

        public void DisableText()
        {
            textObjetive.gameObject.SetActive(false);
        }
    }

    public List<Objetive> objetives;

    public void UpdateDisplayObjetives(int indexObjetive)
    {
        if (indexObjetive < 0 || indexObjetive >= objetives.Count)
            return;

        objetives[indexObjetive].UpdateDrawObjetive();
    }

    public void AddCurrentCountObjectToObjetive(int indexObjetive)
    {
        if (indexObjetive < 0 || indexObjetive >= objetives.Count)
            return;

        objetives[indexObjetive].AddCurrentCountObjectToObjetive();
    }

    public void CheckCompleteObjetiveForTakeObjects(int indexObjetive)
    {
        if (indexObjetive < 0 || indexObjetive >= objetives.Count)
            return;

        objetives[indexObjetive].CheckCompleteObjetiveForTakeObjects();
    }

    public void SetObjetiveCompleteTrue(int indexObjetive)
    {
        if (indexObjetive < 0 || indexObjetive >= objetives.Count)
            return;

        objetives[indexObjetive].SetObjectiveComplete(true);
    }

    public void SetObjetiveCompleteFalse(int indexObjetive)
    {
        if (indexObjetive < 0 || indexObjetive >= objetives.Count)
            return;

        objetives[indexObjetive].SetObjectiveComplete(false);
    }

    public void ActivateObjetivesInScreen(int indexObjetive)
    {
        if (indexObjetive < 0 || indexObjetive >= objetives.Count)
            return;

        objetives[indexObjetive].ActiveText();
    }

    public void DisableObjetivesInScreen(int indexObjetive)
    {
        if (indexObjetive < 0 || indexObjetive >= objetives.Count)
            return;

        objetives[indexObjetive].DisableText();
    }

    public void DisableObjetivesInScreen()
    {
        for (int i = 0; i < objetives.Count; i++)
        {
            objetives[i].DisableText();
        }
    }
    public void ActivateObjetivesInScreen()
    {
        for (int i = 0; i < objetives.Count; i++)
        {
            objetives[i].ActiveText();
        }
    }
}
