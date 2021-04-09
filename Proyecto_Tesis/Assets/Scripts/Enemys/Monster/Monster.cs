using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Monster : Updateable
{
    public enum Monster_STATES
    {
        Idle,
        AssignedCurrentWaypoint,
        GoToWaypoint,
        Wait,
        ChasePlayer,
        KillPlayer,
        Die,
        Count,
    }

    public enum Monster_EVENTS
    {
        Start,
        DoneAssignedCurrentWaypoint,
        DoneGoToWaypoint,
        DoneDelayWait,
        PlayerInRangeSight,
        PlayerOutRangeSight,
        PlayerInRangeDead,
        DestroyEnemy,
        PlayerDead,
        Count,
    }
    // Start is called before the first frame update

    public enum TypeFinderWaypoint
    {
        NearPlayer,
        DistantPlayer,
        RandomPlayer
    }
    
    private FinderWaypoints finderWaypoints;
    [SerializeField] private TypeFinderWaypoint typeFinderWaypoint = TypeFinderWaypoint.RandomPlayer;
    [SerializeField] private int countWaypointsNearPlayer = 3;
    [SerializeField] private int countWaypointsDistancePlayer = 3;

    [SerializeField] private float speedPatrol;
    [SerializeField] private float speedChasePlayer;
    [SerializeField] private float delayWaitInPatrol;
    [SerializeField] private float auxDelayWaitInPatrol;
    [SerializeField] private float rangeKillPlayer;

    [SerializeField] private float rangeMagnitudeWaypoint = 0.5f;

    [SerializeField] private float delayOutChase = 2.5f;
    [SerializeField] private float auxDelayOutChase = 2.5f;
    [SerializeField] private float rangeChasePlayer = 10.0f;
    [SerializeField] private float rangeRayCastCheckChasePlayer;
    [SerializeField] private float distanceBeetwoenRayCastChasePlayer = 0.5f;
    [SerializeField] private Transform spawnRayCastCheckChasePlayer;

    [SerializeField] private List<Transform> waypoints;
    private List<Transform> auxWaypoints;

    [SerializeField] private Transform currentWaypoit;
    [SerializeField] private Transform currentPlayer;
    [SerializeField] private NavMeshAgent navMesh;

    [SerializeField] private UnityEvent OnKillPlayer;

    private bool resetBehaviour = false;
    private bool destroyEnemy = false;
    private FSM fsmMonster;
    
    void Awake()
    {
        resetBehaviour = false;
        destroyEnemy = false;

        finderWaypoints = new FinderWaypoints();

        fsmMonster = new FSM((int)Monster_STATES.Count, (int)Monster_EVENTS.Count, (int)Monster_STATES.Idle);

        fsmMonster.SetRelations((int)Monster_STATES.Idle, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.Start);

        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.GoToWaypoint, (int)Monster_EVENTS.DoneAssignedCurrentWaypoint);

        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.Wait, (int)Monster_EVENTS.DoneGoToWaypoint);

        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.DoneDelayWait);

        fsmMonster.SetRelations((int)Monster_STATES.ChasePlayer, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.PlayerOutRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.ChasePlayer, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.ChasePlayer, (int)Monster_STATES.KillPlayer, (int)Monster_EVENTS.PlayerInRangeDead);

        fsmMonster.SetRelations((int)Monster_STATES.KillPlayer, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.PlayerDead);
    }
    protected override void Start()
    {
        auxWaypoints = waypoints;

        base.Start();
        MyUpdate.AddListener(UpdateMonster);
        UM.UpdatesInGame.Add(MyUpdate);
    }
    public void UpdateMonster()
    {
        switch (fsmMonster.GetCurrentState())
        {
            case (int)Monster_STATES.Idle:
                Idle();
                break;
            case (int)Monster_STATES.AssignedCurrentWaypoint:
                AssignedCurrentWaypoint();
                break;
            case (int)Monster_STATES.GoToWaypoint:
                GoToWaypoint();
                break;
            case (int)Monster_STATES.Wait:
                Wait();
                break;
            case (int)Monster_STATES.ChasePlayer:
                ChasePlayer();
                break;
            case (int)Monster_STATES.Die:
                Die();
                break;
            case (int)Monster_STATES.KillPlayer:
                KillPlayer();
                break;
        }
    }

    private void Idle()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.Start);
    }

    private void AssignedCurrentWaypoint()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;
        navMesh.isStopped = true;
        navMesh.speed = 0;
        navMesh.acceleration = 1000;

        Transform newWaypoint = null;

        waypoints = auxWaypoints;

        switch (typeFinderWaypoint)
        {
            case TypeFinderWaypoint.RandomPlayer:
                newWaypoint = finderWaypoints.GetNonRepeatedWaypoint(currentWaypoit, waypoints);
                break;
            case TypeFinderWaypoint.NearPlayer:
                waypoints = finderWaypoints.GetListWaypointsNearTarget(auxWaypoints, currentPlayer, countWaypointsNearPlayer);
                newWaypoint = finderWaypoints.GetNonRepeatedWaypoint(currentWaypoit, waypoints);
                break;
            case TypeFinderWaypoint.DistantPlayer:

                break;
        }

        currentWaypoit = newWaypoint;

        if (currentWaypoit != null)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.DoneAssignedCurrentWaypoint);
        }

        CheckInChasePlayer();
        CheckDead();
    }

    private void GoToWaypoint()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;
        navMesh.isStopped = false;
        navMesh.speed = speedPatrol;
        navMesh.acceleration = speedPatrol * 2;
        navMesh.SetDestination(currentWaypoit.position);

        float distance = Vector3.Distance(transform.position, currentWaypoit.position);

        if (distance <= rangeMagnitudeWaypoint)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.DoneGoToWaypoint);
        }
        CheckInChasePlayer();
        CheckDead();
    }

    private void Wait()
    {
        navMesh.isStopped = true;
        navMesh.speed = 0;
        navMesh.acceleration = 1000;

        if (delayWaitInPatrol > 0)
        {
            delayWaitInPatrol = delayWaitInPatrol - Time.deltaTime;
        }
        else
        {
            delayWaitInPatrol = auxDelayWaitInPatrol;
            fsmMonster.SendEvent((int)Monster_EVENTS.DoneDelayWait);
        }

        CheckInChasePlayer();
        CheckDead();
    }

    private void ChasePlayer()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;
        navMesh.isStopped = false;
        navMesh.speed = speedChasePlayer;
        navMesh.acceleration = speedChasePlayer * 2;
        navMesh.SetDestination(currentPlayer.position);

        CheckOutChasePlayer();

        float distanceOfPlayer = Vector3.Distance(transform.position, currentPlayer.position);

        if (distanceOfPlayer <= rangeKillPlayer)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.PlayerInRangeDead);
        }

        CheckDead();
    }

    private void Die()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;

        //REEMPLAZAR ESTO POR LA ANIMACION DE MUERTE DEL ENEMIGO.
        Destroy(gameObject);
    }

    private void KillPlayer()
    {
        delayWaitInPatrol = auxDelayWaitInPatrol;

        OnKillPlayer?.Invoke();

        if (resetBehaviour)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.PlayerDead);
        }
        /*A COMPLETAR.*/
    }

    private void CheckInChasePlayer()
    {
        RaycastHit hit;

        RaycastHit hit2;

        RaycastHit hit3;

        RaycastHit hit4;

        Vector3 direction = currentPlayer.position - transform.position;
        //Debug.DrawLine(transform.position, direction, Color.red, 0.1f);

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position, spawnRayCastCheckChasePlayer.forward, out hit, rangeRayCastCheckChasePlayer))
        {
            if (hit.transform.CompareTag("Player"))
            {
                fsmMonster.SendEvent((int)Monster_EVENTS.PlayerInRangeSight);
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position + new Vector3(distanceBeetwoenRayCastChasePlayer, 0, 0)
            , spawnRayCastCheckChasePlayer.forward, out hit2, rangeRayCastCheckChasePlayer))
        {
            if (hit2.transform.CompareTag("Player"))
            {
                fsmMonster.SendEvent((int)Monster_EVENTS.PlayerInRangeSight);
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position - new Vector3(distanceBeetwoenRayCastChasePlayer, 0, 0)
            , spawnRayCastCheckChasePlayer.forward, out hit3, rangeRayCastCheckChasePlayer))
        {
            if (hit3.transform.CompareTag("Player"))
            {
                fsmMonster.SendEvent((int)Monster_EVENTS.PlayerInRangeSight);
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position, direction, out hit4, rangeChasePlayer))
        {
            if (hit4.transform.CompareTag("Player"))
            {
                fsmMonster.SendEvent((int)Monster_EVENTS.PlayerInRangeSight);
            }
        }
    }
    private void CheckOutChasePlayer()
    {
        RaycastHit hit;

        RaycastHit hit2;

        RaycastHit hit3;

        RaycastHit hit4;

        Vector3 direction = currentPlayer.position - transform.position;

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position, spawnRayCastCheckChasePlayer.forward, out hit, rangeRayCastCheckChasePlayer))
        {
            if (hit.transform.CompareTag("Player"))
            {
                delayOutChase = auxDelayOutChase;
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position + new Vector3(distanceBeetwoenRayCastChasePlayer, 0, 0)
            , spawnRayCastCheckChasePlayer.forward, out hit2, rangeRayCastCheckChasePlayer))
        {
            if (hit2.transform.CompareTag("Player"))
            {
                delayOutChase = auxDelayOutChase;
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position - new Vector3(distanceBeetwoenRayCastChasePlayer, 0, 0)
            , spawnRayCastCheckChasePlayer.forward, out hit3, rangeRayCastCheckChasePlayer))
        {
            if (hit3.transform.CompareTag("Player"))
            {
                delayOutChase = auxDelayOutChase;
            }
        }

        if (Physics.Raycast(transform.position, direction.normalized, out hit4, rangeChasePlayer))
        {
            if (hit4.transform.CompareTag("Player"))
            {
                delayOutChase = auxDelayOutChase;
            }
        }
        CheckDelayOutChasePlayer();
    }

    private void CheckDelayOutChasePlayer()
    {
        if (delayOutChase > 0)
        {
            delayOutChase = delayOutChase - Time.deltaTime;
        }
        else
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.PlayerOutRangeSight);
            delayOutChase = auxDelayOutChase;
        }
    }

    private void CheckDead()
    {
        if (destroyEnemy)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.DestroyEnemy);
        }
    }

    public void SendEventPlayerInRangeSight()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.PlayerInRangeSight);
    }

    public void DestroyEnemy() { destroyEnemy = true; }

    public void SetResetBehaviour(bool value) => resetBehaviour = value;

    public bool GetResetBehaviour() { return resetBehaviour; }
}
