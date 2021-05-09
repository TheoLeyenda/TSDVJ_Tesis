using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System;
public class Monster : Updateable
{
    public enum Monster_STATES
    {
        Null,
        Idle,
        VERDE,
        AMARILLO,
        ROJO,
        NEGRO,
        ATURDIDO,
        TryKillPlayer,
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
        ChangeIdle,
        ChangeStateVERDE,
        ChangeStateAMARILLO,
        ChangeStateROJO,
        ChangeStateNEGRO,
        ChangeStateATURDIDO,
        ChangeStateKillPlayer,
        ChangeStateDie,
        Count,
    }

    [Header("Config estado VERDE")]
    [SerializeField] private float speedPatrol;

    [Header("Config estado AMARILLO")]
    [SerializeField] private float speedRegisterZone;
    [SerializeField] private float timeViewPlayerForSuspecting = 0;
    [SerializeField] private float minDelayRegisterZone = 10;
    [SerializeField] private float maxDelayRegisterZone = 20;
    private float valueSound = 0;
    private float timeViewPlayer = 0;
    [SerializeField] private float delayRegisterZone = 0;
    [SerializeField] private float PorcentageFindRandomWaypointsInStateAMARILLO = 80.0f;
    private float auxDelayRegisterZone = 0;

    [Header("Config estado ROJO")]
    [SerializeField] private float speedChasePlayer = 0;
    [SerializeField] private float speedFindPlayer = 0;
    [SerializeField] private float rangeKillPlayer = 0;
    [SerializeField] private float delayFindPlayerInStateROJO = 9.0f;
    [SerializeField] private float delayOutChase = 2.5f;
    [SerializeField] private AudioClip audioClipRugido;
    [SerializeField] private int countPlayAudioAgitado;
    [SerializeField] private List<AudioClip> audioClipsGemidosAgitados;
    [SerializeField] private float minDelaySoundAgitado;
    [SerializeField] private float maxDelaySoundAgitado;
    [SerializeField] private float delayRugido;
    [SerializeField] private float PorcentageFindRandomWaypointsInStateROJO = 35.0f;
    private int auxCountPlayAudioAgitado;
    private float delaySoundAgitado;
    private float durationClipRugido;
    private float auxDelayOutChase = 2.5f;
    private float auxDelayRugido;
    private bool enableRugido;

    [Header("Config estado ATURDIDO")]
    [SerializeField] private float delayAturdido;
    private float auxDelayAturdido;

    [Header("Config estado NEGRO")]
    [SerializeField] private List<AudioClip> audioClipsGrito;
    [SerializeField] private float speedFurioso;
    [SerializeField] private float delayFindTargetInStateNEGRO;
    [SerializeField] private float minDelayGrito;
    [SerializeField] private float maxDelayGrito;
    [SerializeField] private float PorcentageFindRandomWaypointsInStateNEGRO;
    private float delayGrito;
    private float durationGRITO;

    [Header("Dependencias de Scripts")]
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] private MoveToWaypoint moveToWaypoint;
    [SerializeField] private PatrolBehavior patrolBehavior;
    [SerializeField] private ListeningSound listeningSound;
    [SerializeField] private ViewFlashlight viewFlashlight;
    [SerializeField] private RegisterZone registerZone = null;
    [SerializeField] private RangeVision rangeVision = null;
    [SerializeField] private CasterValueForLevel casterValueForLevelSound;
    [SerializeField] private CasterValueForLevel casterValueForLevelView;
    [SerializeField] private Stuneable stuneable;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UnityEvent OnKillPlayer = null;

    [Header("General Data")]
    private bool resetBehaviour = false;
    private bool destroyEnemy = false;
    private Vector3 lastPositionViewTarget;
    private FSM fsmMonster;
    private bool doneGoToLastPositionTarget = false;
    private bool outChaseTarget = false;
    private bool enableStuneMeInCaughtTarget = false;
    public static event Action<Transform, float> OnSendStuneTargetEvent;
    public static event Action<Transform> OnOutStuneTarget;
    private bool sendStuneTargetEvent;

    [SerializeField] private float delayStuneTarget;
    [SerializeField] private TargetGeneratePerimeterRegisterZone targetGeneratePerimeterRegisterZone;
    [SerializeField] private Transform currentTargetMonster;
    [SerializeField] private float delayToKillPlayer;
    private float auxDelayToKillPlayer;

    void Awake()
    {
        resetBehaviour = false;
        destroyEnemy = false;
        sendStuneTargetEvent = false;
        //finderWaypoints = new FinderWaypoints();

        fsmMonster = new FSM((int)Monster_STATES.Count, (int)Monster_EVENTS.Count, (int)Monster_STATES.Idle);

        fsmMonster.SetRelations((int)Monster_STATES.Idle, (int)Monster_STATES.VERDE, (int)Monster_EVENTS.Start);

        fsmMonster.SetRelations((int)Monster_STATES.VERDE, (int)Monster_STATES.Idle, (int)Monster_EVENTS.ChangeIdle);
        fsmMonster.SetRelations((int)Monster_STATES.VERDE, (int)Monster_STATES.AMARILLO, (int)Monster_EVENTS.ChangeStateAMARILLO);
        fsmMonster.SetRelations((int)Monster_STATES.VERDE, (int)Monster_STATES.ROJO, (int)Monster_EVENTS.ChangeStateROJO);
        fsmMonster.SetRelations((int)Monster_STATES.VERDE, (int)Monster_STATES.NEGRO, (int)Monster_EVENTS.ChangeStateNEGRO);
        fsmMonster.SetRelations((int)Monster_STATES.VERDE, (int)Monster_STATES.TryKillPlayer, (int)Monster_EVENTS.ChangeStateKillPlayer);
        fsmMonster.SetRelations((int)Monster_STATES.VERDE, (int)Monster_STATES.Die, (int)Monster_EVENTS.ChangeStateDie);
        fsmMonster.SetRelations((int)Monster_STATES.VERDE, (int)Monster_STATES.ATURDIDO, (int)Monster_EVENTS.ChangeStateATURDIDO);

        fsmMonster.SetRelations((int)Monster_STATES.AMARILLO, (int)Monster_STATES.Idle, (int)Monster_EVENTS.ChangeIdle);
        fsmMonster.SetRelations((int)Monster_STATES.AMARILLO, (int)Monster_STATES.VERDE, (int)Monster_EVENTS.ChangeStateVERDE);
        fsmMonster.SetRelations((int)Monster_STATES.AMARILLO, (int)Monster_STATES.ROJO, (int)Monster_EVENTS.ChangeStateROJO);
        fsmMonster.SetRelations((int)Monster_STATES.AMARILLO, (int)Monster_STATES.NEGRO, (int)Monster_EVENTS.ChangeStateNEGRO);
        fsmMonster.SetRelations((int)Monster_STATES.AMARILLO, (int)Monster_STATES.TryKillPlayer, (int)Monster_EVENTS.ChangeStateKillPlayer);
        fsmMonster.SetRelations((int)Monster_STATES.AMARILLO, (int)Monster_STATES.Die, (int)Monster_EVENTS.ChangeStateDie);
        fsmMonster.SetRelations((int)Monster_STATES.AMARILLO, (int)Monster_STATES.ATURDIDO, (int)Monster_EVENTS.ChangeStateATURDIDO);

        fsmMonster.SetRelations((int)Monster_STATES.ROJO, (int)Monster_STATES.Idle, (int)Monster_EVENTS.ChangeIdle);
        fsmMonster.SetRelations((int)Monster_STATES.ROJO, (int)Monster_STATES.VERDE, (int)Monster_EVENTS.ChangeStateVERDE);
        fsmMonster.SetRelations((int)Monster_STATES.ROJO, (int)Monster_STATES.AMARILLO, (int)Monster_EVENTS.ChangeStateAMARILLO);
        fsmMonster.SetRelations((int)Monster_STATES.ROJO, (int)Monster_STATES.NEGRO, (int)Monster_EVENTS.ChangeStateNEGRO);
        fsmMonster.SetRelations((int)Monster_STATES.ROJO, (int)Monster_STATES.TryKillPlayer, (int)Monster_EVENTS.ChangeStateKillPlayer);
        fsmMonster.SetRelations((int)Monster_STATES.ROJO, (int)Monster_STATES.Die, (int)Monster_EVENTS.ChangeStateDie);
        fsmMonster.SetRelations((int)Monster_STATES.ROJO, (int)Monster_STATES.ATURDIDO, (int)Monster_EVENTS.ChangeStateATURDIDO);

        fsmMonster.SetRelations((int)Monster_STATES.NEGRO, (int)Monster_STATES.Idle, (int)Monster_EVENTS.ChangeIdle);
        fsmMonster.SetRelations((int)Monster_STATES.NEGRO, (int)Monster_STATES.VERDE, (int)Monster_EVENTS.ChangeStateVERDE);
        fsmMonster.SetRelations((int)Monster_STATES.NEGRO, (int)Monster_STATES.AMARILLO, (int)Monster_EVENTS.ChangeStateAMARILLO);
        fsmMonster.SetRelations((int)Monster_STATES.NEGRO, (int)Monster_STATES.ROJO, (int)Monster_EVENTS.ChangeStateROJO);
        fsmMonster.SetRelations((int)Monster_STATES.NEGRO, (int)Monster_STATES.TryKillPlayer, (int)Monster_EVENTS.ChangeStateKillPlayer);
        fsmMonster.SetRelations((int)Monster_STATES.NEGRO, (int)Monster_STATES.Die, (int)Monster_EVENTS.ChangeStateDie);
        fsmMonster.SetRelations((int)Monster_STATES.NEGRO, (int)Monster_STATES.ATURDIDO, (int)Monster_EVENTS.ChangeStateATURDIDO);

        fsmMonster.SetRelations((int)Monster_STATES.ATURDIDO, (int)Monster_STATES.Idle, (int)Monster_EVENTS.ChangeIdle);
        fsmMonster.SetRelations((int)Monster_STATES.ATURDIDO, (int)Monster_STATES.VERDE, (int)Monster_EVENTS.ChangeStateVERDE);
        fsmMonster.SetRelations((int)Monster_STATES.ATURDIDO, (int)Monster_STATES.AMARILLO, (int)Monster_EVENTS.ChangeStateAMARILLO);
        fsmMonster.SetRelations((int)Monster_STATES.ATURDIDO, (int)Monster_STATES.ROJO, (int)Monster_EVENTS.ChangeStateROJO);
        fsmMonster.SetRelations((int)Monster_STATES.ATURDIDO, (int)Monster_STATES.NEGRO, (int)Monster_EVENTS.ChangeStateNEGRO);
        fsmMonster.SetRelations((int)Monster_STATES.ATURDIDO, (int)Monster_STATES.TryKillPlayer, (int)Monster_EVENTS.ChangeStateKillPlayer);
        fsmMonster.SetRelations((int)Monster_STATES.ATURDIDO, (int)Monster_STATES.Die, (int)Monster_EVENTS.ChangeStateDie);

        fsmMonster.SetRelations((int)Monster_STATES.TryKillPlayer, (int)Monster_STATES.VERDE, (int)Monster_EVENTS.ChangeStateVERDE);
        fsmMonster.SetRelations((int)Monster_STATES.TryKillPlayer, (int)Monster_STATES.ATURDIDO, (int)Monster_EVENTS.ChangeStateATURDIDO);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ListeningSound.OnListeningSoundAction += CheckChangeStateByListeningSound;
        ViewFlashlight.OnViewFlashAction += CheckChangeStateByViewFlashlight;
        CheckSendEventStune.OnSendEventStune += CheckTryScapeMonster;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ListeningSound.OnListeningSoundAction -= CheckChangeStateByListeningSound;
        ViewFlashlight.OnViewFlashAction -= CheckChangeStateByViewFlashlight;
        CheckSendEventStune.OnSendEventStune -= CheckTryScapeMonster;
    }

    protected override void Start()
    {
        durationClipRugido = 0;
        auxDelayOutChase = delayOutChase;
        auxDelayRegisterZone = delayRegisterZone;
        auxDelayRugido = delayRugido;
        auxDelayToKillPlayer = delayToKillPlayer;
        auxDelayAturdido = delayAturdido;
        registerZone.DisableRegisterObject();

        auxCountPlayAudioAgitado = countPlayAudioAgitado;

        delayGrito = 0;

        delaySoundAgitado = UnityEngine.Random.Range(minDelaySoundAgitado, maxDelaySoundAgitado);

        base.Start();
        MyUpdate.AddListener(UpdateMonster);
        UM.UpdatesInGame.Add(MyUpdate);
        lastPositionViewTarget = Vector3.zero;
    }
    public void UpdateMonster()
    {
        
        switch (fsmMonster.GetCurrentState())
        {
            case (int)Monster_STATES.Idle:
                Idle();
                break;
            case (int)Monster_STATES.VERDE:
                VERDE();
                break;
            case (int)Monster_STATES.AMARILLO:
                AMARILLO();
                break;
            case (int)Monster_STATES.ROJO:
                ROJO();
                break;
            case (int)Monster_STATES.NEGRO:
                NEGRO();
                break;
            case (int)Monster_STATES.TryKillPlayer:
                TryKillPlayer();
                break;
            case (int)Monster_STATES.Die:
                Die();
                break;
            case (int)Monster_STATES.ATURDIDO:
                ATURDIDO();
                break;
        }
    }

    private void Idle()
    {
        outChaseTarget = false;
        enableRugido = true;
        doneGoToLastPositionTarget = false;
        delayRegisterZone = auxDelayRegisterZone;
        patrolBehavior.ResetDelayWaitInPatrol();

        fsmMonster.SendEvent((int)Monster_EVENTS.Start);
    }

    private void VERDE()
    {
        //Debug.Log("VERDE");
        
        // FUNCIONA.
        stuneable.SetInStune(false);
        durationClipRugido = 0;
        outChaseTarget = false;
        enableRugido = true;
        sendStuneTargetEvent = false;
        doneGoToLastPositionTarget = false;
        delayRegisterZone = auxDelayRegisterZone;

        navMeshAgent.speed = speedPatrol;
        navMeshAgent.acceleration = speedPatrol * 2;

        if(!patrolBehavior.GetStartBehaviour())
            patrolBehavior.StartBehaviour();

        rangeVision.UpdateRangeVision();
        patrolBehavior.UpdatePatrolBehavior();
        //Debug.Log("Time view player: " + timeViewPlayer);
        if (CheckInSuspectingPlayer(Monster_EVENTS.ChangeStateAMARILLO) 
            || CheckChasePlayerForTimeViewPlayer(Monster_EVENTS.ChangeStateROJO) 
            || CheckDead(Monster_EVENTS.ChangeStateAMARILLO)
            || valueSound > 0)
        {

            float delayForSound = casterValueForLevelSound.GetValueForLevel(valueSound);
            float delayForView = 0;
            
            if(timeViewPlayer > 0)
                delayForView = casterValueForLevelView.GetValueForLevel(timeViewPlayer);

            //Debug.Log(delayForSound + "+" + delayForView);
            delayRegisterZone = delayForSound + delayForView;

            //Debug.Log(delayRegisterZone);

            if (delayRegisterZone < minDelayRegisterZone)
                delayRegisterZone = minDelayRegisterZone;

            if (delayRegisterZone > maxDelayRegisterZone)
                delayRegisterZone = maxDelayRegisterZone;

            //Debug.Log("delayRegisterZone: "+ delayRegisterZone);

            patrolBehavior.StopBehaviour();
            timeViewPlayer = 0;
            valueSound = 0;
        }
    }

    private void AMARILLO()
    {
        //Debug.Log("AMARILLO");
        
        //FUNCIONA
        durationClipRugido = 0;
        outChaseTarget = false;
        enableRugido = true;
        sendStuneTargetEvent = false;
        patrolBehavior.ResetDelayWaitInPatrol();
        registerZone.SetPorcentageRandomTarget(PorcentageFindRandomWaypointsInStateAMARILLO);
        registerZone.SetSpeedMovementRegister(speedRegisterZone);

        CheckGoToLastPositionPlayer();

        if (doneGoToLastPositionTarget)
        {
            RegisterZone(Monster_EVENTS.ChangeStateVERDE, Monster_EVENTS.ChangeStateROJO);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
                Debug.Log(doneGoToLastPositionTarget);
        }

    }

    private void ROJO()
    {
        //Debug.Log("ROJO");

        sendStuneTargetEvent = false;
        navMeshAgent.SetDestination(currentTargetMonster.position);
        registerZone.SetPorcentageRandomTarget(PorcentageFindRandomWaypointsInStateROJO);

        if (enableRugido)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
            navMeshAgent.acceleration = 1000;
        }
        else
        {
            if (!outChaseTarget)
            {
                countPlayAudioAgitado = auxCountPlayAudioAgitado;
                delaySoundAgitado = 0;
                delayRegisterZone = delayFindPlayerInStateROJO;
                navMeshAgent.isStopped = false;
                navMeshAgent.speed = speedChasePlayer;
                navMeshAgent.acceleration = speedChasePlayer * 2;
                outChaseTarget = CheckDelayOutChasePlayer(Monster_EVENTS.Null, false);
            }

            if (outChaseTarget)
            {
                
                registerZone.SetSpeedMovementRegister(speedFindPlayer);
                outChaseTarget = !RegisterZone(Monster_EVENTS.ChangeStateAMARILLO, Monster_EVENTS.Null);
                if (delaySoundAgitado > 0 && countPlayAudioAgitado > 0)
                {
                    delaySoundAgitado = delaySoundAgitado - Time.deltaTime;
                }
                else if(countPlayAudioAgitado > 0)
                {
                    if (audioClipsGemidosAgitados.Count > 0)
                    {
                        int index = UnityEngine.Random.Range(0, audioClipsGemidosAgitados.Count);
                        audioSource.PlayOneShot(audioClipsGemidosAgitados[index]);
                        delaySoundAgitado = audioClipsGemidosAgitados[index].length;
                        delaySoundAgitado = delaySoundAgitado + UnityEngine.Random.Range(minDelaySoundAgitado, maxDelaySoundAgitado);
                        countPlayAudioAgitado--;
                        
                    }

                }

                if (outChaseTarget)
                {
                    doneGoToLastPositionTarget = true;
                }
            }

            CheckTryKillPlayer(Monster_EVENTS.ChangeStateKillPlayer, true);
        }

        CheckSoundRugido();
        
    }

    private void ATURDIDO()
    {
        //Debug.Log("ATURDIDO");
        outChaseTarget = false;
        //Debug.Log(delayAturdido);
        if (delayAturdido > 0 
            && fsmMonster.GetCurrentState() == (int)Monster_STATES.ATURDIDO)
        {
            stuneable.SetInStune(true);
            navMeshAgent.speed = 0;
            navMeshAgent.acceleration = 1000;
            navMeshAgent.isStopped = true;

            delayAturdido = delayAturdido - Time.deltaTime;
        }
        else if(delayAturdido <= 0)
        {
            delayAturdido = auxDelayAturdido;
            stuneable.SetDelayStune(0.0f);
            stuneable.SetInStune(false);
            delayGrito = 0;
            fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateNEGRO);
        }
       
    }

    private void NEGRO()
    {
        //Debug.Log("NEGRO");
        int indexAudio = 0;

        if (!outChaseTarget)
        {
            navMeshAgent.SetDestination(currentTargetMonster.position);
            delayRegisterZone = delayFindTargetInStateNEGRO;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speedFurioso;
            navMeshAgent.acceleration = speedFurioso * 2;
            outChaseTarget = CheckDelayOutChasePlayer(Monster_EVENTS.Null, false);

        }

        if (outChaseTarget)
        {
            registerZone.SetSpeedMovementRegister(speedFurioso);
            registerZone.SetPorcentageRandomTarget(PorcentageFindRandomWaypointsInStateNEGRO);
            outChaseTarget = !RegisterZone(Monster_EVENTS.ChangeStateAMARILLO, Monster_EVENTS.Null);

            if (outChaseTarget)
            {
                doneGoToLastPositionTarget = true;
            }
        }

        CheckTryKillPlayer(Monster_EVENTS.ChangeStateKillPlayer, false);

        if (audioClipsGrito.Count > 0)
        {
            if (delayGrito > 0)
            {
                delayGrito = delayGrito - Time.deltaTime;
            }
            else
            {
                indexAudio = UnityEngine.Random.Range(0, audioClipsGrito.Count);
                audioSource.PlayOneShot(audioClipsGrito[indexAudio]);
                delayGrito = UnityEngine.Random.Range(minDelayGrito, maxDelayGrito) + audioClipsGrito[indexAudio].length;
            }
        }
    }

    //Devuelve true si encuentra al player.
    private bool RegisterZone(Monster_EVENTS outDelayRegisterZoneEvent, Monster_EVENTS targetFoundEvent)
    {
        //Debug.Log("REGISTER ZONE");

        bool foundPlayer = false;

        switch (targetGeneratePerimeterRegisterZone)
        {
            case TargetGeneratePerimeterRegisterZone.Monster:
                registerZone.SetGeneraterPerimeterRegisterObject(gameObject);
                break;
            case TargetGeneratePerimeterRegisterZone.Player:
                registerZone.SetGeneraterPerimeterRegisterObject(currentTargetMonster.gameObject);
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
            doneGoToLastPositionTarget = false;
            delayRegisterZone = auxDelayRegisterZone;
            registerZone.SetEnableRegisterZone(false);
            registerZone.DisableRegisterObject();
            registerZone.ResetFoundTarget();

            //ESPECIFICAR A QUE ESTADO CAMBIARIA.

            //Debug.Log("En teoria deberia ir al estado amarillo (?");
            fsmMonster.SendEvent((int)outDelayRegisterZoneEvent);
        }

        if (registerZone.GetFoundTarget())
        {
            doneGoToLastPositionTarget = false;
            delayRegisterZone = auxDelayRegisterZone;
            registerZone.SetEnableRegisterZone(false);
            registerZone.DisableRegisterObject();
            registerZone.ResetFoundTarget();
            foundPlayer = true;
            //ESPECIFICAR A QUE ESTADO CAMBIARIA.
            fsmMonster.SendEvent((int)targetFoundEvent);
        }

        registerZone.UpdateRegisterZone();
        return foundPlayer;
    }

    private void CheckGoToLastPositionPlayer()
    {
        if (!doneGoToLastPositionTarget)
            doneGoToLastPositionTarget = GoToLastPositionPlayer();
    }

    //Retornara true cuando haya llegado a la ultima posicion donde estaba el player
    private bool GoToLastPositionPlayer()
    {
        if (lastPositionViewTarget == Vector3.zero)
        {
            lastPositionViewTarget = new Vector3(currentTargetMonster.position.x, transform.position.y, currentTargetMonster.position.z);
            moveToWaypoint.StartBehaviour(lastPositionViewTarget);
        }

        moveToWaypoint.UpdateMoveToWaypoint();

        bool isArrived = moveToWaypoint.CheckArrivedTarget();

        if (isArrived)
            lastPositionViewTarget = Vector3.zero;

        return isArrived;

    }

    private void Die()
    {
        lastPositionViewTarget = Vector3.zero;
        delayRegisterZone = auxDelayRegisterZone;
        //REEMPLAZAR ESTO POR LA ANIMACION DE MUERTE DEL ENEMIGO.
        Destroy(gameObject);
    }

    private void CheckTryKillPlayer(Monster_EVENTS eventKillPlayer, bool _enableStune)
    {
        float distanceOfPlayer = Vector3.Distance(transform.position, currentTargetMonster.position);

        enableStuneMeInCaughtTarget = _enableStune;
        sendStuneTargetEvent = false;

        if (distanceOfPlayer <= rangeKillPlayer)
        {
            fsmMonster.SendEvent((int)eventKillPlayer);
        }
        else
        {
            delayToKillPlayer = auxDelayToKillPlayer;
        }
    }

    private void TryKillPlayer()
    {
        lastPositionViewTarget = Vector3.zero;
        delayRegisterZone = auxDelayRegisterZone;
        delayAturdido = auxDelayAturdido;

        if(!sendStuneTargetEvent)
        {
            if (OnSendStuneTargetEvent != null)
                OnSendStuneTargetEvent(currentTargetMonster, delayStuneTarget);

            sendStuneTargetEvent = true;
        }

        if (delayToKillPlayer > 0)
        {
            delayToKillPlayer = delayToKillPlayer - Time.deltaTime;
            if (stuneable.GetInStune() && enableStuneMeInCaughtTarget)
            {
                fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateATURDIDO);

                if (OnOutStuneTarget != null)
                    OnOutStuneTarget(currentTargetMonster);

                delayToKillPlayer = auxDelayToKillPlayer;

                //Debug.Log("ENTRE AL ATURDIMIENTO");
            }
            else if (!enableStuneMeInCaughtTarget)
            {
                stuneable.SetInStune(false);

                //Debug.Log("ENTRE AL NO ATURDIMIENTO");
            }
        }
        else if(!stuneable.GetInStune())
        {
            //stuneable.SetInStune(false);
            delayToKillPlayer = auxDelayToKillPlayer;
            OnKillPlayer?.Invoke();

            Debug.Log("Llegue a asesinar brutalmente al player");

            if (resetBehaviour)
            {
                fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateVERDE);
            }
        }
    }

    private void CheckTryScapeMonster(CheckSendEventStune checkSendEventStune)
    {
        if (checkSendEventStune.GetMyUserTrasform() == currentTargetMonster 
            && fsmMonster.GetCurrentState() == (int)Monster_STATES.TryKillPlayer)
        {
            checkSendEventStune.SetEnableUseSendEventStune(false);
            stuneable.SetInStune(true);
        }
    }

    private void CheckSoundRugido()
    {
        if (enableRugido)
        {
            if (delayRugido > 0)
            {
                if (durationClipRugido > 0)
                {
                    durationClipRugido = durationClipRugido - Time.deltaTime;
                }
                else
                {
                    //Debug.Log("CONCHA TU MADREE");
                    durationClipRugido = audioClipRugido.length;
                    audioSource.PlayOneShot(audioClipRugido);
                }
                delayRugido = delayRugido - Time.deltaTime;
            }
            else
            {
                delayRugido = auxDelayRugido;
                enableRugido = false;
            }
        }
    }

    private bool CheckInSuspectingPlayer(Monster_EVENTS sendEvent)
    {
        //Si lo ve persigue al player
        bool viewPlayer = CheckViewPlayer(Monster_EVENTS.Null, false);

        if (!viewPlayer)
        {
            if (timeViewPlayer > 0 && timeViewPlayer <= timeViewPlayerForSuspecting)
            {
                //ESPECIFICAR A QUE ESTADO CAMBIARIA.
                fsmMonster.SendEvent((int)sendEvent);
                //timeViewPlayer = 0;
                return true;
            }
        }

        return false;
    }

    private bool CheckChasePlayerForTimeViewPlayer(Monster_EVENTS sendEvent)
    {
        CheckViewPlayer(Monster_EVENTS.Null, false);

        if (timeViewPlayer > timeViewPlayerForSuspecting)
        {
            //ESPECIFICAR A QUE ESTADO CAMBIARIA.
            //Debug.Log("ENTRE");
            CheckViewPlayer(sendEvent, true);
            //timeViewPlayer = 0;
            return true;
        }
        return false;
    }

    private void CheckChangeStateByViewFlashlight(ViewFlashlight _viewFlashlight)
    {
        if (viewFlashlight != _viewFlashlight)
            return;

        switch (fsmMonster.GetCurrentState())
        {
            case (int)Monster_STATES.Idle:
                break;
            case (int)Monster_STATES.VERDE:
                fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateROJO);
                break;
            case (int)Monster_STATES.AMARILLO:
                fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateROJO);
                outChaseTarget = false;
                delayOutChase = auxDelayOutChase;
                delayRugido = auxDelayRugido;
                break;
            case (int)Monster_STATES.ROJO:
                if (outChaseTarget)
                {
                    outChaseTarget = false;
                    delayRugido = auxDelayRugido;
                    delayOutChase = auxDelayOutChase;
                }
                break;
            case (int)Monster_STATES.NEGRO:
                if (outChaseTarget)
                {
                    outChaseTarget = false;
                    delayOutChase = auxDelayOutChase;
                }
                break;
            case (int)Monster_STATES.TryKillPlayer:
                break;
            case (int)Monster_STATES.Die:
                break;
            case (int)Monster_STATES.ATURDIDO:
                break;
        }
    }

    private void CheckChangeStateByListeningSound(ListeningSound listening, float volumeSound)
    {
        if (listening != listeningSound)
            return;

        valueSound = volumeSound;

        switch (fsmMonster.GetCurrentState())
        {
            case (int)Monster_STATES.Idle:
                break;
            case (int)Monster_STATES.VERDE:
                fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateAMARILLO);
                VERDE();
                break;
            case (int)Monster_STATES.AMARILLO:
                if (doneGoToLastPositionTarget)
                {
                    fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateROJO);
                    outChaseTarget = false;
                    delayRugido = auxDelayRugido;
                    delayOutChase = auxDelayOutChase;
                }

                break;
            case (int)Monster_STATES.ROJO:
                if (outChaseTarget)
                {
                    outChaseTarget = false;
                    delayRugido = auxDelayRugido;
                    delayOutChase = auxDelayOutChase;
                }
                break;
            case (int)Monster_STATES.NEGRO:
                if (outChaseTarget)
                {
                    outChaseTarget = false;
                    delayOutChase = auxDelayOutChase;
                }
                break;
            case (int)Monster_STATES.TryKillPlayer:
                break;
            case (int)Monster_STATES.Die:
                break;
            case (int)Monster_STATES.ATURDIDO:
                break;
        }
    }

    private bool CheckViewPlayer(Monster_EVENTS sendEvent, bool useSendEvent)
    {
        //Debug.DrawLine(transform.position, direction, Color.red, 0.1f);
        bool playerView = false;
        rangeVision.UpdateRangeVision();
        playerView = rangeVision.CheckViewTargetForTransform(currentTargetMonster);

        if (playerView)
        {
            timeViewPlayer += Time.deltaTime;
            //Debug.Log(timeViewPlayer);
        }

        if (useSendEvent && playerView)
        {
            fsmMonster.SendEvent((int)sendEvent);
        }

        return playerView;
        
    }

    //Retorna true si el delay delayOutChase llega a 0.
    private bool CheckDelayOutChasePlayer(Monster_EVENTS sendEvent, bool useSendEvent)
    {
        bool playerView = false;
        rangeVision.UpdateRangeVision();
        playerView = rangeVision.CheckViewTargetForTransform(currentTargetMonster);

        if (playerView)
            delayOutChase = auxDelayOutChase;

        if (delayOutChase > 0)
        {
            delayOutChase = delayOutChase - Time.deltaTime;
        }
        else
        {
            //ESPECIFICAR A QUE ESTADO CAMBIARIA.
            if (useSendEvent)
            {
                fsmMonster.SendEvent((int)sendEvent);
            }
            delayOutChase = auxDelayOutChase;
            return true;
        }
        return false;
    }

    private bool CheckDead(Monster_EVENTS sendEvent)
    {
        if (destroyEnemy)
        {
            //ESPECIFICAR A QUE ESTADO CAMBIARIA.
            fsmMonster.SendEvent((int)sendEvent);
            return true;
        }
        return false;
    }

    public void SendEventChangeStateIdle()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.ChangeIdle);
    }

    public void SendEventChangeStateVERDE()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateVERDE);
    }

    public void SendEventChangeStateAMARILLO()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateAMARILLO);
    }

    public void SendEventChangeStateROJO()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateROJO);
    }

    public void SendEventChangeStateNEGRO()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateNEGRO);
    }

    public void SendEventChangeStateKillPlayer()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateKillPlayer);
    }

    public void SendEventChangeStateDie()
    {
        fsmMonster.SendEvent((int)Monster_EVENTS.ChangeStateDie);
    }

    private void ResetDoneGoToLastPositionPlayer() => doneGoToLastPositionTarget = false;

    public void ResetDelayOutChasePlayer()
    {
        delayOutChase = auxDelayOutChase;
    }
    public void DestroyEnemy() { destroyEnemy = true; }

    public void SetResetBehaviour(bool value) => resetBehaviour = value;

    public bool GetResetBehaviour() { return resetBehaviour; }

}
