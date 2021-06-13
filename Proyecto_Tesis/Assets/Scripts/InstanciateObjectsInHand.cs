using UnityEngine;

public class InstanciateObjectsInHand : MonoBehaviour
{
    [SerializeField] private Transform SpawnTransform = null;
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

    public void InstanciatedObjectInHand(GameObject objectInstance)
    {

        if (SpawnTransform == null)
            return;


        if (objectInstance == CurrentOriginObject && CurrentOriginObject != null)
        {
            DestroyCurrentInstanciateObject();
        }
        else
        {
            //Debug.Log(objectInstance);
            DestroyCurrentInstanciateObject();
            CurrentOriginObject = objectInstance;
            CurrentInstanciateObject = Instantiate(objectInstance, SpawnTransform.position, SpawnTransform.rotation, parent);
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
