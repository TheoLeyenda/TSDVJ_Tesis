using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionOnPlaceObjects : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPlace
    {
        [System.Serializable]
        public class SlotPlace
        {
            //Objecto que se ve colocado.
            public GameObject visibleObject;

            //Objecto Transparentado.
            public GameObject transparentObject;

            public int Id_Object;

        }
        public SlotPlace[] arrSlotsPlace;

        //Item que nesesito del jugador.
        public GameObject compareObject;

        private int currentIndexVisibleObjectOn = -1;

        public void SwitchObjectVisible(int id)
        {
            GameObject tokenActivated = null;
            for (int i = 0; i < arrSlotsPlace.Length; i++)
            {
                if (arrSlotsPlace[i].visibleObject.activeSelf)
                {
                    tokenActivated = arrSlotsPlace[i].visibleObject;
                    i = arrSlotsPlace.Length;
                }
            }
            for (int i = 0; i < arrSlotsPlace.Length; i++)
            {
                if (arrSlotsPlace[i].Id_Object == id)
                {
                    bool visible = !arrSlotsPlace[i].visibleObject.activeSelf;
                    bool changeToken = (tokenActivated != arrSlotsPlace[i].visibleObject && arrSlotsPlace[i].visibleObject);
                    if (visible || changeToken)
                    {
                        currentIndexVisibleObjectOn = i;
                        arrSlotsPlace[i].visibleObject.SetActive(true);
                        arrSlotsPlace[i].transparentObject.SetActive(false);
                    }
                    else
                    {
                        arrSlotsPlace[i].visibleObject.SetActive(false);
                        arrSlotsPlace[i].transparentObject.SetActive(true);
                    }
                }
                else
                {
                    arrSlotsPlace[i].visibleObject.SetActive(false);
                }
            }
        }
        
    }

    [System.Serializable]
    public class ItemsPlayer
    {
        public GameObject objectItem;
        public int Id_Object;
    }

    [SerializeField] private ObjectPlace[] answerObjectsPlace;
    [SerializeField] private ItemsPlayer[] itemsPlayer;

    [SerializeField] private UnityEvent eventOnCorrectPlaceObject;

    private int currentIndexObjectPlace = -1;

    public void SetCurrentIndexObjectPlace(int value) => currentIndexObjectPlace = value;

    public void AddOrRemoveObjectPlace()
    {
        int index = -1;
       
        for (int i = 0; i < itemsPlayer.Length; i++)
        {
            if (itemsPlayer[i].objectItem.activeSelf)
            {
                index = i;
                i = itemsPlayer.Length;
            }
        }

        if (index == -1 || currentIndexObjectPlace < 0 || currentIndexObjectPlace >= answerObjectsPlace.Length)
            return;

        answerObjectsPlace[currentIndexObjectPlace].SwitchObjectVisible(itemsPlayer[index].Id_Object);
        
    }
}
