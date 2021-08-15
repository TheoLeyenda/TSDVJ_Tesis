using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonsManager : Updateable
{
    public EventSystem eventSystem;
    [SerializeField] private List<Selectable> buttons;

    private Selectable currentButtonSelect;
    private Selectable lastButtonSelect;
    // Start is called before the first frame update
    protected override void Start()
    {
        lastButtonSelect = null;
        if(currentButtonSelect != null)
            currentButtonSelect = buttons[0];
        base.Start();
        MyUpdate.AddListener(UpdateButtonsManager);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    // Update is called once per frame
    void UpdateButtonsManager()
    {
        if (buttons.Count <= 0 || eventSystem == null)
            return;

        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].interactable)
            {
                lastButtonSelect = currentButtonSelect;
                currentButtonSelect = buttons[i];
                i = buttons.Count;
            }
        }
        if (currentButtonSelect != lastButtonSelect)
        {
            if (currentButtonSelect != null)
            {
                eventSystem.firstSelectedGameObject = currentButtonSelect.gameObject;
                currentButtonSelect.Select();
            }
        }
    }
}
