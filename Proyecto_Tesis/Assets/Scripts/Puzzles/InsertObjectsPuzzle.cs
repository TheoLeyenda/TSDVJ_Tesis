using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertObjectsPuzzle : MonoBehaviour
{
    public class Slot
    {
        public Transform positionInsertObject;
        public string nameObjectInsert;
        public bool insert;
    }

    [SerializeField] private string[] resultNameObjectsOrder;
    [SerializeField] private Slot[] slots;
    [SerializeField] private GameObject SelectorObject;
}
