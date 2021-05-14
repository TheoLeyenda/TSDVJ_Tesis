using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public enum TypeFinderWaypoint
    {
        NearTarget,
        DistantTarget,
        RandomWaypoint
    }

    public enum PatrolBehaviour_STATES
    {
        Idle,
        AssignedCurrentWaypoint,
        GoToWaypoint,
        Wait,
        Count,
    }

    public enum PatrolBehaviour_EVENTS
    {
        Start,
        DoneAssignedCurrentWaypoint,
        DoneGoToWaypoint,
        DoneDelayWait,
        ResetBehaviour,
        Count,
    }

    [SerializeField] private float speedPatrol = 0;
    private FinderWaypoints finderWaypoints;
    [SerializeField] private TypeFinderWaypoint typeFinderWaypoint = TypeFinderWaypoint.RandomWaypoint;
    [SerializeField] private int countWaypointsNearTarget = 3;
    [SerializeField] private int countWaypointsDistanceTarget = 3;
    [SerializeField] private float delayWaitInPatrol = 0;
    private float auxDelayWaitInPatrol;
    [SerializeField] private float rangeMagnitudeWaypoint = 0.5f;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private List<Transform> auxWaypoints;
    [SerializeField] private Transform currentWaypoit = null;
    [SerializeField] private Transform currentTarget = null;
    private FSM fsmPatrolBehavior;
    [SerializeField] private bool startBehaviour = false;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private NavMeshPath path;

    void Awake()
    {
        finderWaypoints = new FinderWaypoints();
        fsmPatrolBehavior = new FSM((int)PatrolBehaviour_STATES.Count, (int)PatrolBehaviour_EVENTS.Count, (int)PatrolBehaviour_STATES.Idle);

        fsmPatrolBehavior.SetRelations((int)PatrolBehaviour_STATES.Idle, (int)PatrolBehaviour_STATES.AssignedCurrentWaypoint, (int)PatrolBehaviour_EVENTS.Start);

        fsmPatrolBehavior.SetRelations((int)PatrolBehaviour_STATES.AssignedCurrentWaypoint, (int)PatrolBehaviour_STATES.Idle, (int)PatrolBehaviour_EVENTS.ResetBehaviour);
        fsmPatrolBehavior.SetRelations((int)PatrolBehaviour_STATES.AssignedCurrentWaypoint, (int)PatrolBehaviour_STATES.GoToWaypoint, (int)PatrolBehaviour_EVENTS.DoneAssignedCurrentWaypoint);

        fsmPatrolBehavior.SetRelations((int)PatrolBehaviour_STATES.GoToWaypoint, (int)PatrolBehaviour_STATES.Idle, (int)PatrolBehaviour_EVENTS.ResetBehaviour);
        fsmPatrolBehavior.SetRelations((int)PatrolBehaviour_STATES.GoToWaypoint, (int)PatrolBehaviour_STATES.Wait, (int)PatrolBehaviour_EVENTS.DoneGoToWaypoint);

        fsmPatrolBehavior.SetRelations((int)PatrolBehaviour_STATES.Wait, (int)PatrolBehaviour_STATES.Idle, (int)PatrolBehaviour_EVENTS.ResetBehaviour);
        fsmPatrolBehavior.SetRelations((int)PatrolBehaviour_STATES.Wait, (int)PatrolBehaviour_STATES.AssignedCurrentWaypoint, (int)PatrolBehaviour_EVENTS.DoneDelayWait);
    }

    void Start()
    {
        path = new NavMeshPath();
        auxDelayWaitInPatrol = delayWaitInPatrol;
        auxWaypoints = waypoints;
    }

    public void UpdatePatrolBehavior()
    {
        if (!startBehaviour)
            return;

        switch (fsmPatrolBehavior.GetCurrentState())
        {
            case (int)PatrolBehaviour_STATES.Idle:
                Idle();
                break;
            case (int)PatrolBehaviour_STATES.AssignedCurrentWaypoint:
                AssignedCurrentWaypoint();
                break;
            case (int)PatrolBehaviour_STATES.GoToWaypoint:
                GoToWaypoint();
                break;
            case (int)PatrolBehaviour_STATES.Wait:
                Wait();
                break;
        }
    }

    void Idle()
    {
        if (startBehaviour)
        {
            fsmPatrolBehavior.SendEvent((int)PatrolBehaviour_EVENTS.Start);
        }
    }

    void AssignedCurrentWaypoint()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        navMeshAgent.acceleration = 1000;

        Transform newWaypoint = null;

        waypoints = auxWaypoints;

        switch (typeFinderWaypoint)
        {
            case TypeFinderWaypoint.RandomWaypoint:
                newWaypoint = finderWaypoints.GetNonRepeatedWaypoint(currentWaypoit, waypoints);
                break;
            case TypeFinderWaypoint.NearTarget:
                waypoints = finderWaypoints.GetListWaypointsNearTarget(auxWaypoints, currentTarget, countWaypointsNearTarget);
                newWaypoint = finderWaypoints.GetNonRepeatedWaypoint(currentWaypoit, waypoints);
                break;
            case TypeFinderWaypoint.DistantTarget:
                waypoints = finderWaypoints.GetListWaypointsDistantTarget(auxWaypoints, currentTarget, countWaypointsDistanceTarget);
                newWaypoint = finderWaypoints.GetNonRepeatedWaypoint(currentWaypoit, waypoints);
                break;
        }

        currentWaypoit = newWaypoint;

        if (currentWaypoit != null)
        {
            fsmPatrolBehavior.SendEvent((int)PatrolBehaviour_EVENTS.DoneAssignedCurrentWaypoint);
        }
    }

    void GoToWaypoint()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedPatrol;
        navMeshAgent.acceleration = speedPatrol * 2;
        CreatingNewPath(currentWaypoit);

        if (navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            //Debug.Log("No hay ruta");
            navMeshAgent.isStopped = true;

            List<Transform> localWaypoints = new List<Transform>();
            localWaypoints.Clear();
            for (int i = 0; i < auxWaypoints.Count; i++)
            {
                CreatingNewPath(auxWaypoints[i]);
                if (navMeshAgent.hasPath)
                {
                    localWaypoints.Add(auxWaypoints[i]);
                }
            }

            Debug.Log(localWaypoints.Count);
            if (localWaypoints.Count > 0)
            {
                int index = 0;
                index = Random.Range(0, localWaypoints.Count);

                currentWaypoit = localWaypoints[index];
            }
        }

        float distance = Vector3.Distance(transform.position, currentWaypoit.position);

        if (distance <= rangeMagnitudeWaypoint)
        {
            fsmPatrolBehavior.SendEvent((int)PatrolBehaviour_EVENTS.DoneGoToWaypoint);
        }
    }

    void Wait()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        navMeshAgent.acceleration = 1000;

        if (delayWaitInPatrol > 0)
        {
            delayWaitInPatrol = delayWaitInPatrol - Time.deltaTime;
        }
        else
        {
            delayWaitInPatrol = auxDelayWaitInPatrol;
            fsmPatrolBehavior.SendEvent((int)PatrolBehaviour_EVENTS.DoneDelayWait);
        }
    }

    public void StartBehaviour()
    {
        fsmPatrolBehavior.SendEvent((int)PatrolBehaviour_EVENTS.ResetBehaviour);
        startBehaviour = true;
    }

    public void StopBehaviour()
    {
        fsmPatrolBehavior.SendEvent((int)PatrolBehaviour_EVENTS.ResetBehaviour);
        startBehaviour = false;
    }

    public PatrolBehaviour_STATES GetCurrentStatePatrolBehaviour()
    {
        return (PatrolBehaviour_STATES)fsmPatrolBehavior.GetCurrentState();
    }

    public bool GetStartBehaviour()
    {
        return startBehaviour;
    }

    public void ResetDelayWaitInPatrol()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;
    }

    public void CreatingNewPath(Transform destinationPath)
    {
        navMeshAgent.CalculatePath(destinationPath.position, path);
        navMeshAgent.SetPath(path);
    }
}
