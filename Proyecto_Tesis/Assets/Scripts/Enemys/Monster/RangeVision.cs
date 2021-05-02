using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeVision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private FildOfView fildOfView;

    public void UpdateRangeVision()
    {
        fildOfView.FindVisibleTargets();
    }

    public bool CheckViewTargetForTransform(Transform targetFind)
    {
        bool found = false;

        for (int i = 0; i < fildOfView.visibleTargets.Count; i++)
        {
            if (fildOfView.visibleTargets[i] == targetFind)
            {
                found = true;
                i = fildOfView.visibleTargets.Count;
            }
        }

        return found;
    }
}
