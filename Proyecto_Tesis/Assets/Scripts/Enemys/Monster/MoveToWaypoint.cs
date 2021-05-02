using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToWaypoint : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 currentTarget;
    [SerializeField] private float magnitudeArrivedWaypoint = 0.5f;
    private bool isRunning = false;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float speedMoveToWaypoint = 0;

    // Update is called once per frame
    public void UpdateMoveToWaypoint()
    {
        if (!isRunning)
            return;


        if (!CheckArrivedTarget())
        {
            isRunning = false;
            Movement();
        }
    }

    void Movement()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedMoveToWaypoint;
        navMeshAgent.acceleration = speedMoveToWaypoint * 2;
        navMeshAgent.SetDestination(currentTarget);
    }

    public bool CheckArrivedTarget()
    {
        float currentDistance = Vector3.Distance(transform.position, currentTarget);

        return currentDistance <= magnitudeArrivedWaypoint;
    }

    public void StartBehaviour() => isRunning = true;

    public void StartBehaviour(Vector3 newTarget)
    {
        isRunning = true;
        currentTarget = newTarget;
    }

    public void FinishBehaviour()
    {
        isRunning = false;
        currentTarget = Vector3.zero;
    }

    public void SetCurrentTarget(Vector3 newTarget)
    {
        currentTarget = newTarget;
    }
}
