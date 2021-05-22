using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UpdateManager : MonoBehaviour
{
    [HideInInspector] public List<UnityEvent> UpdatesInGame;
    [HideInInspector] public List<UnityEvent> FixedUpdatesInGame;
    public static UpdateManager instanceUpdateManager;

    void Awake()
    {
        if (instanceUpdateManager == null)
        {
            instanceUpdateManager = this;
        }
        else if (instanceUpdateManager != null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckUpdatesUnityEventsInGame();
    }
    void FixedUpdate()
    {
        CheckFixedUpdatesUnityEventsInGame();
    }
    private void CheckFixedUpdatesUnityEventsInGame()
    {
        for (int i = 0; i < FixedUpdatesInGame.Count; i++)
        {
            if (FixedUpdatesInGame[i] != null)
                FixedUpdatesInGame[i].Invoke();
            else
                FixedUpdatesInGame.Remove(FixedUpdatesInGame[i]);
        }
    }
    private void CheckUpdatesUnityEventsInGame()
    {
        for (int i = 0; i < UpdatesInGame.Count; i++)
        {
            if (UpdatesInGame[i] != null)
                UpdatesInGame[i].Invoke();
            else
                UpdatesInGame.Remove(UpdatesInGame[i]);
        }
    }
}
