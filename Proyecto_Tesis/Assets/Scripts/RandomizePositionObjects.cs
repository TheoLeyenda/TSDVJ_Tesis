using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePositionObjects : MonoBehaviour
{
    [SerializeField] private bool enableRepeatPositions = false;
    [SerializeField] private bool enableRepeatNumberUbication = false;
    [SerializeField] private bool randomizeInStart = true;
    [SerializeField] private bool useParents = false;


    [SerializeField] private List<GameObject> Objects;
    [SerializeField] private List<AreaSpawnObject> ubicationUses;

    [System.Serializable]
    public class AreaSpawnObject
    {
        public Transform transform;
        public int numberUbication;
    }

    void Start()
    {
        if (randomizeInStart)
        {
            RandomizeObjects();
        }
    }

    public void RandomizeObjects()
    {
        List<int> indexUbicationUses = new List<int>();
        for (int i = 0; i < Objects.Count; i++)
        {
            int indexPosition = Random.Range(0, ubicationUses.Count);
            Objects[i].transform.position = ubicationUses[indexPosition].transform.position;
            Objects[i].transform.rotation = ubicationUses[indexPosition].transform.rotation;
            int currentNumberUbication = ubicationUses[indexPosition].numberUbication;
            if (useParents)
            {
                Objects[i].transform.parent = ubicationUses[indexPosition].transform;
            }
            if (!enableRepeatPositions)
            {
                ubicationUses.Remove(ubicationUses[indexPosition]);
            }

            if (!enableRepeatNumberUbication)
            {
                for (int j = 0; j < ubicationUses.Count; j++)
                {
                    if(currentNumberUbication == ubicationUses[j].numberUbication)
                    {
                        indexUbicationUses.Add(j);
                    }
                }

                for (int j = 0; j < indexUbicationUses.Count; j++)
                {
                    ubicationUses.Remove(ubicationUses[indexUbicationUses[j]]);

                    for (int k = 0; k < indexUbicationUses.Count; k++)
                    {
                        if(indexUbicationUses[k] > 0)
                        indexUbicationUses[k]--;
                    }

                    //indexUbicationUses.Remove(indexUbicationUses[j]);

                }
                indexUbicationUses.Clear();
            }
        }
    }
}
