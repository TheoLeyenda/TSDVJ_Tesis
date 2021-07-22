using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateObjectsInHand : MonoBehaviour
{
    [SerializeField] private List<Transform> SpawnTransform = null;
    private int indexTransformUse = 0;
    [SerializeField] private Transform parent;
    private GameObject CurrentInstanciateObject = null;
    private GameObject CurrentOriginObject = null;
    //[SerializeField] private GameObject[] objectsForInstanciate;

    //public void InstanciatedObjectInHand(int indexObject)
    //{
    //    if (indexObject > objectsForInstanciate.Length - 1 || indexObject < 0 || SpawnTransform == null)
    //        return;

    //    if (CurrentInstanciateObject != null)
    //    {
    //        Destroy(CurrentInstanciateObject);
    //        CurrentInstanciateObject = null;
    //    }

    //    CurrentInstanciateObject = Instantiate(objectsForInstanciate[indexObject], SpawnTransform.position, SpawnTransform.rotation, parent);
    //}

    public void SetIndexTransformUse(int value) => indexTransformUse = value;

    public void InstanciatedObjectInHand(GameObject objectInstance)
    {
        Debug.Log("Me instancie vieja: " + objectInstance.name);

        if (SpawnTransform == null)
            return;


        if (objectInstance == CurrentOriginObject && CurrentOriginObject != null)
        {
            Debug.Log("PUTA");
            DestroyCurrentInstanciateObject();
        }
        else
        {
            //Debug.Log(objectInstance);
            DestroyCurrentInstanciateObject();
            CurrentOriginObject = objectInstance;
            CurrentInstanciateObject = Instantiate(objectInstance, SpawnTransform[indexTransformUse].position, SpawnTransform[indexTransformUse].rotation, parent);

            CurrentInstanciateObject.layer = 19; //Esto es para no tener que poner una referencia en esta clase, por favor no te enojes Theito-Kun U//n//U

            Transform[] children = CurrentInstanciateObject.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                child.gameObject.layer = 19;
            }
        }
    }

    public void InstanciatedObjectInHand(GameObject objectInstance, bool enableDestroyInstanciatedObject)
    {

        if (SpawnTransform == null)
            return;

        if (objectInstance == CurrentOriginObject && CurrentOriginObject != null && enableDestroyInstanciatedObject)
        {
            Debug.Log("PUTA");
            DestroyCurrentInstanciateObject();
        }
        else
        {
            //Debug.Log(objectInstance);
            DestroyCurrentInstanciateObject();
            CurrentOriginObject = objectInstance;
            CurrentInstanciateObject = Instantiate(objectInstance, SpawnTransform[indexTransformUse].position, SpawnTransform[indexTransformUse].rotation, parent);

            CurrentInstanciateObject.layer = 19; //Esto es para no tener que poner una referencia en esta clase, por favor no te enojes Theito-Kun U//n//U

            Transform[] children = CurrentInstanciateObject.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                child.gameObject.layer = 19;
            }
        }
    }

    public void DestroyCurrentInstanciateObject()
    {
        if (CurrentInstanciateObject != null)
        {
            Destroy(CurrentInstanciateObject);
            CurrentInstanciateObject = null;
            CurrentOriginObject = null;
        }
    }

    public GameObject GetCurrentOriginObject() { return CurrentOriginObject; }

    public GameObject GetCurrentInstanciateObject() { return CurrentInstanciateObject; }
}
