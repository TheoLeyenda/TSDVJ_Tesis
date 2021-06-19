using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material[] materials;

    [SerializeField] private MeshRenderer meshRenderer;

    public void ChangeMat(int indexMatAssigned)
    {
        if (indexMatAssigned < 0 || indexMatAssigned >= materials.Length)
            return;

        meshRenderer.material = materials[indexMatAssigned];
    }
}
