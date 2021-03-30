using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
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
    [SerializeField] private float speedPatrol;
    [SerializeField] private float speedChasePlayer;
    [SerializeField] private float delayWaitInPatrol;
    [SerializeField] private float auxDelayWaitInPatrol;
    [SerializeField] private float rangeKillPlayer;
    [SerializeField] private float rangeChasePlayer;

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private Transform currentWaypoit;
    [SerializeField] private List<Transform> playersInGame;
    [SerializeField] private Transform currentPlayer;
    [SerializeField] private NavMeshAgent navMesh;

    private FSM fsmMonster;
    void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        switch (fsmMonster.GetCurrentState())
        {
            case (int)Monster_STATES.Idle:
                break;
            case (int)Monster_STATES.AssignedCurrentWaypoint:
                break;
            case (int)Monster_STATES.GoToWaypoint:
                break;
            case (int)Monster_STATES.Wait:
                break;
            case (int)Monster_STATES.ChasePlayer:
                break;
            case (int)Monster_STATES.Die:
                break;
            case (int)Monster_STATES.KillPlayer:
                break;
        }
    }

    private void Idle() { }

    private void AssignedCurrentWaypoint() { }

    private void GoToWaypoint() { }

    private void Wait() { }

    private void ChasePlayer() { }

    private void Die() { }

    private void KillPlayer() { }

}
