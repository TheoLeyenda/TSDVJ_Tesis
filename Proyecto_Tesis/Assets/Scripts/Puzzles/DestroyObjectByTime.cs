using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectByTime : MonoBehaviour
{
    public float lifeTime = 5.0f;

    public GameObject objectDestryed;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(objectDestryed);
    }
}
