using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Monster : Updateable
{
    public enum Monster_STATES
    {
        Null,
        Idle,
        AssignedCurrentWaypoint,
        GoToWaypoint,
        Wait,
        GoToLastPositionPlayerView,
        RegisterZone,
        ChasePlayer,
        KillPlayer,
        Die,
        Count,
    }
    public enum TargetGeneratePerimeterRegisterZone
    {
        Player,
        Monster,
    }
    public enum Monster_EVENTS
    {
        Null,
        Start,
        DoneAssignedCurrentWaypoint,
        DoneGoToWaypoint,
        DoneDelayWait,
        InSuspecting,
        OutSuspecting,
        DoneGoToLastPositionPlayerView,
        PlayerInRangeSight,
        PlayerOutRangeSight,
        PlayerInRangeDead,
        DestroyEnemy,
        OnLisentPlayer,
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

    [SerializeField] private float speedPatrol = 0;
    [SerializeField] private float speedChasePlayer = 0;
    [SerializeField] private float delayWaitInPatrol = 0;
    private float auxDelayWaitInPatrol;
    [SerializeField] private float rangeKillPlayer = 0;

    [SerializeField] private float rangeMagnitudeWaypoint = 0.5f;

    [SerializeField] private float speedGoToLastedPositionPlayer = 0;
    [SerializeField] private float timeViewPlayerForSuspecting = 0;

    [SerializeField] private float delayOutChase = 2.5f;
    private float auxDelayOutChase = 2.5f;
    [SerializeField] private float rangeChasePlayer = 10.0f;
    [SerializeField] private float rangeRayCastCheckChasePlayer = 0;
    [SerializeField] private float distanceBeetwoenRayCastChasePlayer = 0.5f;
    [SerializeField] private Transform spawnRayCastCheckChasePlayer = null;

    [SerializeField] private List<Transform> waypoints;
    private List<Transform> auxWaypoints;

    [SerializeField] private Transform currentWaypoit = null;
    [SerializeField] private Transform currentPlayer = null;
    [SerializeField] private NavMeshAgent navMesh = null;

    [SerializeField] private UnityEvent OnKillPlayer = null;

    private bool resetBehaviour = false;
    private bool destroyEnemy = false;
    private float timeViewPlayer = 0;
    private Vector3 lastPositionViewPlayer;
    private FSM fsmMonster;

    [SerializeField] private RegisterZone registerZone = null;
    [SerializeField] private float delayRegisterZone = 0;
    private float auxDelayRegisterZone = 0;

    [SerializeField] private TargetGeneratePerimeterRegisterZone targetGeneratePerimeterRegisterZone;

    void Awake()
    {
        resetBehaviour = false;
        destroyEnemy = false;

        finderWaypoints = new FinderWaypoints();

        fsmMonster = new FSM((int)Monster_STATES.Count, (int)Monster_EVENTS.Count, (int)Monster_STATES.Idle);

        fsmMonster.SetRelations((int)Monster_STATES.Idle, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.Start);

        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.GoToWaypoint, (int)Monster_EVENTS.DoneAssignedCurrentWaypoint);
        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_EVENTS.InSuspecting);
        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_EVENTS.OnLisentPlayer);

        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.Wait, (int)Monster_EVENTS.DoneGoToWaypoint);
        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_EVENTS.InSuspecting);
        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.GoToWaypoint, (int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_EVENTS.OnLisentPlayer);

        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.DoneDelayWait);
        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_EVENTS.InSuspecting);
        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.Wait, (int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_EVENTS.OnLisentPlayer);

        fsmMonster.SetRelations((int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_STATES.RegisterZone, (int)Monster_EVENTS.DoneGoToLastPositionPlayerView);
        fsmMonster.SetRelations((int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_STATES.KillPlayer, (int)Monster_EVENTS.PlayerInRangeDead);

        fsmMonster.SetRelations((int)Monster_STATES.RegisterZone, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.PlayerInRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.RegisterZone, (int)Monster_STATES.ChasePlayer, (int)Monster_EVENTS.OnLisentPlayer);
        fsmMonster.SetRelations((int)Monster_STATES.RegisterZone, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.PlayerOutRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.RegisterZone, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.OutSuspecting);
        fsmMonster.SetRelations((int)Monster_STATES.RegisterZone, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.RegisterZone, (int)Monster_STATES.KillPlayer, (int)Monster_EVENTS.PlayerInRangeDead);

        fsmMonster.SetRelations((int)Monster_STATES.ChasePlayer, (int)Monster_STATES.GoToLastPositionPlayerView, (int)Monster_EVENTS.PlayerOutRangeSight);
        fsmMonster.SetRelations((int)Monster_STATES.ChasePlayer, (int)Monster_STATES.Die, (int)Monster_EVENTS.DestroyEnemy);
        fsmMonster.SetRelations((int)Monster_STATES.ChasePlayer, (int)Monster_STATES.KillPlayer, (int)Monster_EVENTS.PlayerInRangeDead);

        fsmMonster.SetRelations((int)Monster_STATES.KillPlayer, (int)Monster_STATES.AssignedCurrentWaypoint, (int)Monster_EVENTS.PlayerDead);
    }
    protected override void Start()
    {
        auxDelayWaitInPatrol = delayWaitInPatrol;
        auxDelayOutChase = delayOutChase;
        auxDelayRegisterZone = delayRegisterZone;

        auxWaypoints = waypoints;

        registerZone.DisableRegisterObject();

        base.Start();
        MyUpdate.AddListener(UpdateMonster);
        UM.UpdatesInGame.Add(MyUpdate);
        lastPositionViewPlayer = Vector3.zero;
    }
    public void UpdateMonster()
    {
        //if (Input.GetKey(KeyCode.Return))
        //{
            Debug.Log((Monster_STATES)fsmMonster.GetCurrentState());
        //}

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
            case (int)Monster_STATES.GoToLastPositionPlayerView:
                GoToLastPositionPlayerView();
                break;
            case (int)Monster_STATES.RegisterZone:
                RegisterZone();
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
        lastPositionViewPlayer = Vector3.zero;
        fsmMonster.SendEvent((int)Monster_EVENTS.Start);
    }

    private void AssignedCurrentWaypoint()
    {
        lastPositionViewPlayer = Vector3.zero;
        delayRegisterZone = auxDelayRegisterZone;
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
                waypoints = finderWaypoints.GetListWaypointsDistantTarget(auxWaypoints, currentPlayer, countWaypointsDistancePlayer);
                newWaypoint = finderWaypoints.GetNonRepeatedWaypoint(currentWaypoit, waypoints);
                break;
        }

        currentWaypoit = newWaypoint;

        if (currentWaypoit != null)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.DoneAssignedCurrentWaypoint);
        }

        CheckInSuspectingPlayer();
        CheckChasePlayerForTimeViewPlayer();

        CheckDead();
    }

    private void GoToWaypoint()
    {
        lastPositionViewPlayer = Vector3.zero;
        delayRegisterZone = auxDelayRegisterZone;
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

        CheckInSuspectingPlayer();
        CheckChasePlayerForTimeViewPlayer();

        CheckDead();
    }

    private void Wait()
    {
        lastPositionViewPlayer = Vector3.zero;
        delayRegisterZone = auxDelayRegisterZone;
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

        CheckInSuspectingPlayer();
        CheckChasePlayerForTimeViewPlayer();

        CheckDead();
    }
    private void GoToLastPositionPlayerView()
    {
        delayRegisterZone = auxDelayRegisterZone;
        navMesh.isStopped = false;
        navMesh.speed = speedGoToLastedPositionPlayer;
        navMesh.acceleration = speedGoToLastedPositionPlayer * 2;

        if (lastPositionViewPlayer == Vector3.zero)
            lastPositionViewPlayer = new Vector3(currentPlayer.position.x, transform.position.y, currentPlayer.position.z);

        navMesh.SetDestination(lastPositionViewPlayer);

        float distanceForTarget = Vector3.Distance(lastPositionViewPlayer, transform.position);
        float magnitude = 0.5f;

        if (distanceForTarget <= magnitude)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.DoneGoToLastPositionPlayerView);
        }
        
        CheckViewPlayer(Monster_EVENTS.PlayerInRangeSight, true);

        CheckDead();

    }
    private void RegisterZone()
    {
        lastPositionViewPlayer = Vector3.zero;
        Debug.Log("REGISTER ZONE");
        timeViewPlayer = 0;
        switch (targetGeneratePerimeterRegisterZone)
        {
            case TargetGeneratePerimeterRegisterZone.Monster:
                registerZone.SetGeneraterPerimeterRegisterObject(gameObject);
                break;
            case TargetGeneratePerimeterRegisterZone.Player:
                registerZone.SetGeneraterPerimeterRegisterObject(currentPlayer.gameObject);
                break;
        }
        registerZone.SetEnableRegisterZone(true);
        registerZone.EnableRegisterObject();

        if (delayRegisterZone > 0)
        {
            delayRegisterZone = delayRegisterZone - Time.deltaTime;
        }
        else
        {
            delayRegisterZone = auxDelayRegisterZone;
            registerZone.SetEnableRegisterZone(false);
            registerZone.DisableRegisterObject();
            registerZone.ResetFoundTarget();
            fsmMonster.SendEvent((int)Monster_EVENTS.OutSuspecting);
        }

        if (registerZone.GetFoundTarget())
        {
            delayRegisterZone = auxDelayRegisterZone;
            registerZone.SetEnableRegisterZone(false);
            registerZone.DisableRegisterObject();
            registerZone.ResetFoundTarget();
            fsmMonster.SendEvent((int)Monster_EVENTS.PlayerInRangeSight);
        }

        registerZone.UpdateRegisterZone();

    }
    private void ChasePlayer()
    {
        lastPositionViewPlayer = Vector3.zero;
        delayRegisterZone = auxDelayRegisterZone;
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
        lastPositionViewPlayer = Vector3.zero;
        delayWaitInPatrol = auxDelayWaitInPatrol;
        delayRegisterZone = auxDelayRegisterZone;
        //REEMPLAZAR ESTO POR LA ANIMACION DE MUERTE DEL ENEMIGO.
        Destroy(gameObject);
    }

    private void KillPlayer()
    {
        lastPositionViewPlayer = Vector3.zero;
        delayWaitInPatrol = auxDelayWaitInPatrol;
        delayRegisterZone = auxDelayRegisterZone;
        OnKillPlayer?.Invoke();

        if (resetBehaviour)
        {
            fsmMonster.SendEvent((int)Monster_EVENTS.PlayerDead);
        }
        /*A COMPLETAR.*/
    }
    private void CheckInSuspectingPlayer()
    {
        //Si lo ve persigue al player
        bool viewPlayer = CheckViewPlayer(Monster_EVENTS.Null, false);

        if (!viewPlayer)
        {
            if (timeViewPlayer > 0 && timeViewPlayer <= timeViewPlayerForSuspecting)
            {
                //Debug.Log("ENTRE");
                fsmMonster.SendEvent((int)Monster_EVENTS.InSuspecting);
                timeViewPlayer = 0;
            }
        }
    }

    private void CheckChasePlayerForTimeViewPlayer()
    {
        CheckViewPlayer(Monster_EVENTS.Null, false);

        if (timeViewPlayer > timeViewPlayerForSuspecting)
        {
            //Debug.Log("ENTRE");
            CheckViewPlayer(Monster_EVENTS.PlayerInRangeSight, true);
            timeViewPlayer = 0;
        }
    }

    private bool CheckViewPlayer(Monster_EVENTS sendEvent, bool useSendEvent)
    {
        RaycastHit hit;

        RaycastHit hit2;

        RaycastHit hit3;

        RaycastHit hit4;

        Vector3 direction = currentPlayer.position - transform.position;
        //Debug.DrawLine(transform.position, direction, Color.red, 0.1f);
        bool playerView = false;
        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position, spawnRayCastCheckChasePlayer.forward, out hit, rangeRayCastCheckChasePlayer))
        {
            if (hit.transform.CompareTag("Player"))
            {
                if (useSendEvent)
                {
                    fsmMonster.SendEvent((int)sendEvent);
                }
                playerView = true;
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position + new Vector3(distanceBeetwoenRayCastChasePlayer, 0, 0)
            , spawnRayCastCheckChasePlayer.forward, out hit2, rangeRayCastCheckChasePlayer))
        {
            if (hit2.transform.CompareTag("Player"))
            {
                if (useSendEvent)
                {
                    fsmMonster.SendEvent((int)sendEvent);
                }
                playerView = true;
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position - new Vector3(distanceBeetwoenRayCastChasePlayer, 0, 0)
            , spawnRayCastCheckChasePlayer.forward, out hit3, rangeRayCastCheckChasePlayer))
        {
            if (hit3.transform.CompareTag("Player"))
            {
                if (useSendEvent)
                {
                    fsmMonster.SendEvent((int)sendEvent);
                }
                playerView = true;
            }
        }

        if (Physics.Raycast(spawnRayCastCheckChasePlayer.position, direction, out hit4, rangeChasePlayer))
        {
            if (hit4.transform.CompareTag("Player"))
            {
                if (useSendEvent)
                {
                    fsmMonster.SendEvent((int)sendEvent);
                }
                playerView = true;
            }
        }

        if (playerView)
            timeViewPlayer += Time.deltaTime;

        return playerView;
        
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

    public void SendEventOnLisentPlayer()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.OnLisentPlayer);
    }

    public void SendEventInSuspecting()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.InSuspecting);
    }

    public void ResetDelayOutChasePlayer()
    {
        delayOutChase = auxDelayOutChase;
    }
    public void DestroyEnemy() { destroyEnemy = true; }

    public void SetResetBehaviour(bool value) => resetBehaviour = value;

    public bool GetResetBehaviour() { return resetBehaviour; }
}
