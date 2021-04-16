using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateObjectsWithAngle : MonoBehaviour
{
    [HideInInspector] public List<GameObject> objectsInstanciated;

    public InstanciateObjectsWithAngle()
    {
        if(objectsInstanciated == null)
            objectsInstanciated = new List<GameObject>();
    }

    void Start()
    {
        if(objectsInstanciated == null)
            objectsInstanciated = new List<GameObject>();
    }

    public void ClearListObjectInstanciated()
    {
        objectsInstanciated.Clear();
    }

    public List<GameObject> InstanciateObjects(int countObjectsInstanciate, float distanceBetweenObjects, GameObject originalSpawnerObject,
                                    Vector3 initialRotation, Vector3 positionSpawner, Transform parent)
    {
        Vector3 currentRotation = initialRotation;
        for (int i = 0; i < countObjectsInstanciate; i++)
        {
            GameObject go = Instantiate(originalSpawnerObject, positionSpawner, Quaternion.identity, parent);
            currentRotation = currentRotation + new Vector3(0, distanceBetweenObjects, 0);
            go.transform.eulerAngles = currentRotation;
            objectsInstanciated.Add(go);
        }

        return objectsInstanciated;
    }


}
