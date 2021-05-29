using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class J_ScreenText : Updateable
{
    public float lifeTime = 2.5f;
    public string openDoorText = "Usaste llave."; //capaz que estaria piola que diga llave del conserje o algo asi
    public string lockedDoorText = "Esta cerrada.";
    public string permalockedDoorText = "Parece atorada.";

    private float timer;
    private bool isTimerRunning = false;

    TextMeshProUGUI text;

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateTimer);
        UM.UpdatesInGame.Add(MyUpdate);

        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTimer()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;

            if (timer >= lifeTime)
            {
                isTimerRunning = false;
                timer = 0;

                HideText();
            }
        }
    }

    public void UpdateAndToggleText(string newText)
    {

        text = gameObject.GetComponent<TextMeshProUGUI>();

        if (text != null)
        {
            SetText(newText);

            if (!isTimerRunning)
            {
                ToggleText();
                isTimerRunning = gameObject.activeSelf;
            }
        }
    }

    public void UpdateText(string newText)
    {

        text = gameObject.GetComponent<TextMeshProUGUI>();

        if (text != null)
        {
            SetText(newText);

            if (!isTimerRunning)
            {
                isTimerRunning = gameObject.activeSelf;
                timer = 0;
            }
        }
    }

    public void SetText(string newText)
    {
        if (text != null)
            text.SetText(newText);
    }

    public void ToggleText()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void HideText()
    {
        if (text != null)
            text.gameObject.SetActive(false);
    }
    public void ShowText()
    {
        if (text != null)
            text.gameObject.SetActive(true);
    }

    /* public void OpenText()
     {
         if (text != null)
         {
             text.SetText(openDoorText);
             ToggleText();
         }
     }

     public void LockedText()
     {
         if (text != null)
         {
             text.SetText(lockedDoorText);
             ToggleText();
         }
     }

     public void PermaLockedText()
     {
         if (text != null)
         {
             Debug.Log("Ah");
             text.SetText(permalockedDoorText);
             ToggleText();
         }
     }*/

    public void SetLifeTimeText(float _time) => lifeTime = _time;

    public void DisableText()
    {
        timer = lifeTime;
        HideText();
    }

}
