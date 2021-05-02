using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterValueForLevel : MonoBehaviour
{
    [System.Serializable]
    public class ValueForLevel
    {
        public float LevelNeed;
        public float valueOut;
    }

    public const float NO_FOUND_VALUE = 0;

    public List<ValueForLevel> valueForLevels;

    void Start()
    {
        SortFromSmallestToLargest();
    }

    public float GetValueForLevel(float InValue)
    {
        int indexValue = 0;
        bool foundValue = false;

        if (valueForLevels.Count <= 0)
            return NO_FOUND_VALUE;

        SortFromSmallestToLargest();

        if (InValue >= valueForLevels[valueForLevels.Count - 1].LevelNeed)
        {
            indexValue = valueForLevels.Count - 1;
            return valueForLevels[indexValue].valueOut;
        }

        for (int i = 0; i < valueForLevels.Count; i++)
        {
            if (InValue >= valueForLevels[i].LevelNeed && InValue < valueForLevels[i+1].LevelNeed)
            {
                indexValue = i;
                foundValue = true;
                break;
            }
        }

        if (foundValue)
            return valueForLevels[indexValue].valueOut;
        else
            return NO_FOUND_VALUE;
    }
    private void SortFromSmallestToLargest()
    {
        ValueForLevel aux = null;

        for (int i = 0; i < valueForLevels.Count; i++)
        {
            for (int j = 0; j < valueForLevels.Count; j++)
            {
                int nextIndex = j + 1;
                if (nextIndex < valueForLevels.Count)
                {
                    if (valueForLevels[j].LevelNeed > valueForLevels[nextIndex].LevelNeed)
                    {
                        aux = valueForLevels[j];
                        valueForLevels[j] = valueForLevels[nextIndex];
                        valueForLevels[nextIndex] = aux;
                    }
                }
            }
        }
    }
}
