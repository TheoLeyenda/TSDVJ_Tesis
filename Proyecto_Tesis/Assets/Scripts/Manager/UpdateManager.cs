using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UpdateManager : MonoBehaviour
{
    [HideInInspector] public List<UnityEvent> UpdatesInGame;
    [HideInInspector] public List<UnityEvent> FixedUpdatesInGame;
    [HideInInspector] public List<UnityEvent> SpecialUpdatesInGame;
    [HideInInspector] public List<UnityEvent> SpecialFixedUpdateInGame;

    public static UpdateManager instanceUpdateManager;

    private GameManager gm;

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
    void Start()
    {
        gm = GameManager.instanceGameManager;
    }
    // Update is called once per frame
    void Update()
    {
        CheckUpdates(SpecialUpdatesInGame);

        Debug.Log(Cursor.visible);

        if (gm.GetIsPauseGame())
            return;

        CheckUpdates(UpdatesInGame);
    }
    void FixedUpdate()
    {
        CheckFixedUpdates(SpecialFixedUpdateInGame);

        if (gm.GetIsPauseGame())
            return;

        CheckFixedUpdates(FixedUpdatesInGame);
    }
    private void CheckFixedUpdates(List<UnityEvent> fixedUpdate)
    {
        for (int i = 0; i < fixedUpdate.Count; i++)
        {
            if (fixedUpdate[i] != null)
                fixedUpdate[i].Invoke();
            else
                fixedUpdate.Remove(fixedUpdate[i]);
        }
    }
    private void CheckUpdates(List<UnityEvent> Update)
    {
        for (int i = 0; i < Update.Count; i++)
        {
            if (Update[i] != null)
                Update[i]?.Invoke();
            else
                Update.Remove(Update[i]);
        }
    }
}
