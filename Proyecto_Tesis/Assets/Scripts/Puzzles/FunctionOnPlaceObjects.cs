using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionOnPlaceObjects : MonoBehaviour
{

    [System.Serializable]
    class PlaceObject
    {
        public GameObject transparentObject;
        [HideInInspector] public ItemAndObject[] originObjects;
        [HideInInspector] public ItemAndObject instanciatedObject = null;
        public int id = -1;
        public bool placeObject = false;
        public Transform transformSpawn;
    }
    [System.Serializable]
    class ItemAndObject
    {
        public GameObject originObject;
        [HideInInspector] public GameObject cloneObject;
        public J_Item itemObject;
        public int id_object;

        public ItemAndObject()
        {
            originObject = null;
            itemObject = null;
            id_object = -1;
        }

        public ItemAndObject(GameObject _originObject, J_Item _itemObject, int _id_object)
        {
            originObject = _originObject;
            itemObject = _itemObject;
            id_object = _id_object;
        }
    }

    [SerializeField] private InstanciateObjectsInHand instanciateObjectsInHand;
    [SerializeField] private bool useCheckInPlaceObject = true;
    [SerializeField] private ItemAndObject[] initOriginObjects;
    [SerializeField] private PlaceObject[] PlacesForObjects;
    [SerializeField] private int[] answerPuzzleIds;
    [SerializeField] private UnityEvent eventOnCorrectPlaceObjects;

    void Start()
    {
        for (int i = 0; i < PlacesForObjects.Length; i++)
        {
            PlacesForObjects[i].originObjects = new ItemAndObject[initOriginObjects.Length];
            PlacesForObjects[i].instanciatedObject = new ItemAndObject();
            for (int j = 0; j < PlacesForObjects[i].originObjects.Length; j++)
            {
                PlacesForObjects[i].originObjects[j] = new ItemAndObject(
                    initOriginObjects[j].originObject
                    , initOriginObjects[j].itemObject
                    , initOriginObjects[j].id_object);
            }
        }
    }

    public bool AddOrRemoveObject(int indexPlaceObject)
    {
        bool result = false;

        if (indexPlaceObject > PlacesForObjects.Length - 1 || indexPlaceObject < 0)
            return false;

        GameObject transparentObject = PlacesForObjects[indexPlaceObject].transparentObject;
        int indexObjectSpawn = -1;

        for (int i = 0; i < PlacesForObjects[indexPlaceObject].originObjects.Length; i++)
        {
            if (PlacesForObjects[indexPlaceObject].originObjects[i].originObject == instanciateObjectsInHand.GetCurrentOriginObject())
            {
                indexObjectSpawn = i;
                i = PlacesForObjects[indexPlaceObject].originObjects.Length;
            }
        }

        if (PlacesForObjects[indexPlaceObject].instanciatedObject.originObject == null)
        {
            if (indexObjectSpawn == -1)
                return false;

            GameObject objectSpawn = PlacesForObjects[indexPlaceObject].originObjects[indexObjectSpawn].originObject;
            J_Item item = PlacesForObjects[indexPlaceObject].originObjects[indexObjectSpawn].itemObject;
            int id = PlacesForObjects[indexPlaceObject].originObjects[indexObjectSpawn].id_object;

            transparentObject.SetActive(false);
            instanciateObjectsInHand.DestroyCurrentInstanciateObject();
            J_inventoryManager.instance.RemoveItem(item);
            PlacesForObjects[indexPlaceObject].instanciatedObject.originObject = objectSpawn;
            PlacesForObjects[indexPlaceObject].instanciatedObject.cloneObject = Instantiate(objectSpawn, PlacesForObjects[indexPlaceObject].transformSpawn);
            PlacesForObjects[indexPlaceObject].instanciatedObject.originObject.transform.localScale = new Vector3(1, 1, 1);
            PlacesForObjects[indexPlaceObject].instanciatedObject.originObject.transform.localPosition = Vector3.zero;
            PlacesForObjects[indexPlaceObject].instanciatedObject.itemObject = item;
            PlacesForObjects[indexPlaceObject].placeObject = true;
            PlacesForObjects[indexPlaceObject].id = id;

            //Debug.Log("ENTRE ACA OP 1");
            result = true;
        }
        else if (instanciateObjectsInHand.GetCurrentInstanciateObject() == null)
        {
            if (!J_inventoryManager.instance.IsInventoryFull())
            {
                transparentObject.SetActive(true);
                instanciateObjectsInHand.InstanciatedObjectInHand(PlacesForObjects[indexPlaceObject].instanciatedObject.originObject);
                J_inventoryManager.instance.AddItem(PlacesForObjects[indexPlaceObject].instanciatedObject.itemObject);

                if (PlacesForObjects[indexPlaceObject].instanciatedObject.cloneObject != null)
                    Destroy(PlacesForObjects[indexPlaceObject].instanciatedObject.cloneObject);

                PlacesForObjects[indexPlaceObject].instanciatedObject.originObject = null;
                PlacesForObjects[indexPlaceObject].placeObject = false;
                PlacesForObjects[indexPlaceObject].id = -1;
            }

            //Debug.Log("ENTRE ACA OP 2");
            result = true;
        }
        else if(PlacesForObjects[indexPlaceObject].instanciatedObject != null && instanciateObjectsInHand.GetCurrentInstanciateObject() != null)
        {
            if (indexPlaceObject < 0 || indexPlaceObject >= PlacesForObjects.Length)
                return false;

            if (indexObjectSpawn == -1)
            {
                //transparentObject.SetActive(true);
                //PlacesForObjects[indexPlaceObject].placeObject = false;
                return false;
            }

            GameObject objectSpawn = PlacesForObjects[indexPlaceObject].originObjects[indexObjectSpawn].originObject;
            J_Item item = PlacesForObjects[indexPlaceObject].originObjects[indexObjectSpawn].itemObject;
            int id = PlacesForObjects[indexPlaceObject].originObjects[indexObjectSpawn].id_object;

            transparentObject.SetActive(false);
            J_inventoryManager.instance.RemoveItem(item);

            instanciateObjectsInHand.InstanciatedObjectInHand(PlacesForObjects[indexPlaceObject].instanciatedObject.originObject, false);
            J_inventoryManager.instance.AddItem(PlacesForObjects[indexPlaceObject].instanciatedObject.itemObject);

            if (PlacesForObjects[indexPlaceObject].instanciatedObject.cloneObject != null)
                Destroy(PlacesForObjects[indexPlaceObject].instanciatedObject.cloneObject);

            PlacesForObjects[indexPlaceObject].instanciatedObject.originObject = null;

            
            PlacesForObjects[indexPlaceObject].instanciatedObject.originObject = objectSpawn;
            PlacesForObjects[indexPlaceObject].instanciatedObject.cloneObject = Instantiate(objectSpawn, PlacesForObjects[indexPlaceObject].transformSpawn);
            PlacesForObjects[indexPlaceObject].instanciatedObject.originObject.transform.localScale = new Vector3(1, 1, 1);
            PlacesForObjects[indexPlaceObject].instanciatedObject.originObject.transform.localPosition = Vector3.zero;
            PlacesForObjects[indexPlaceObject].instanciatedObject.itemObject = item;
            PlacesForObjects[indexPlaceObject].placeObject = true;
            PlacesForObjects[indexPlaceObject].id = id;

            //Debug.Log("ENTRE ACA OP 3");
            result = true;
        }

        if (useCheckInPlaceObject)
        {
            CheckEventOnPlaceObject();
        }

        return result;
    }

    public void CheckEventOnPlaceObject()
    {
        if (answerPuzzleIds.Length != PlacesForObjects.Length)
        {
            Debug.Log("la cantidad \"answerPuzzleIds.Length\" debe ser igual a  \"PlacesForObjects.Length\" ");
            return;
        }

        bool correctAnswer = true;

        for (int i = 0; i < answerPuzzleIds.Length; i++)
        {
            if (!PlacesForObjects[i].placeObject || PlacesForObjects[i].id != answerPuzzleIds[i])
            {
                correctAnswer = false;
                i = answerPuzzleIds.Length;
            }
        }

        if (correctAnswer)
        {
            eventOnCorrectPlaceObjects?.Invoke();
        }
    }
}
