using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderWaypoints : MonoBehaviour
{
    private List<Transform> resultListWaypoints;

    void Start()
    {
        resultListWaypoints = new List<Transform>();
    }

    public FinderWaypoints()
    {
        resultListWaypoints = new List<Transform>();
    }

    //Retorna una lista de transform, la cantidad de transforms que returnara se pasa por el ultimo parametro.
    public List<Transform> GetListWaypointsNearTarget(List<Transform> waypoints, Transform target, int sizeArrayReturn)
    {

        if (waypoints.Count <= 0 || target == null)
            return null;

        resultListWaypoints.Clear();

        bool assignedDone = true;
        float currentDistance = Vector3.Distance(target.position, waypoints[0].position);
        int currentIndexWaypoints = 0;

        if (sizeArrayReturn >= waypoints.Count)
            return waypoints;

        if (sizeArrayReturn <= 0)
            return null;

        for (int i = 0; i < sizeArrayReturn; i++)
        {
            for (int j = 0; j < waypoints.Count; j++)
            {
                bool isWaypointDone = false;
                for (int k = 0; k < resultListWaypoints.Count; k++)
                {
                    if (waypoints[j] == null || waypoints[j] == resultListWaypoints[k])
                    {
                        isWaypointDone = true;
                        k = resultListWaypoints.Count;
                    }
                }
                if (!isWaypointDone)
                {
                    float auxDistance = Vector3.Distance(target.position, waypoints[j].position);
                    if (auxDistance < currentDistance)
                    {
                        assignedDone = true;
                        currentDistance = auxDistance;
                        currentIndexWaypoints = j;
                    }
                }
            }
            if (assignedDone)
            {
                resultListWaypoints.Add(waypoints[currentIndexWaypoints]);
                assignedDone = false;
            }
        }
        return resultListWaypoints;
    }

    public Transform GetNonRepeatedWaypoint(Transform prevWaypoint, List<Transform> waypoints)
    {

        if (waypoints == null || waypoints.Count <= 0)
        {
            Debug.Log("Retorno nulo pibe");
            return null;
        }

        int index = Random.Range(0, waypoints.Count);

        if (prevWaypoint == waypoints[index])
        {
            if (index >= waypoints.Count - 1)
                index = 0;
            else
                index++;
        }

        return waypoints[index];
    }
}
