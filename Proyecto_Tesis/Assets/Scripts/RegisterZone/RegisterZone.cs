using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class RegisterZone : MonoBehaviour
{
    [SerializeField] private GameObject myPrincipalObject;
    [SerializeField] private GameObject GeneratorPerimeterRegisterObject;

    public GameObject registerObject;
    [SerializeField] private List<string> targetsTag = null;
    [SerializeField] private FildOfView fildOfView;
    [SerializeField] private float speedMovementRegister;

    [SerializeField] private float maxRangePerimeterRegister;
    [SerializeField] private LayerMask layerWalls;
    [SerializeField] private LayerMask findObjectLayer;
    [SerializeField] private List<string> targetsSpecificsWaypointsTags;
    [SerializeField] private List<Vector3> specificsTargetsPositions;
    [SerializeField] private float porcentageRandomTarget = 60.0f;

    [SerializeField] private UnityEvent OnArrivedSpecificTarget;

    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private float delayWaitRegister = 2.5f;
    private float auxDelayWaitRegister;

    private Vector3 randomTargetPosition;

    private Vector3 currentTarget;

    private bool foundTarget = false;
    private FSM fsmRegiserZone;

    [SerializeField] private float distanceOfCurrentTarget = 1.2f; 

    private bool enableRegisterZone = false;

    Vector3 centerGizmos = Vector3.zero;
    Vector3 halfExtendedGizmos = Vector3.zero;

    public enum RegisterZone_STATES
    {
        Idle,
        FinderRangePerimeter,
        AssignedPositionMovement,
        GoToRandomPosition,
        GoToSpecificPosition,
        Wait,
        Count,
    }

    public enum RegisterZone_EVENTS
    {
        StartBehaviour,
        DoneFinderRangePerimeter,
        AssignedRandomPosition,
        AssignedSpecificPosition,
        ArrivedPosition,
        OnFinderRangePerimeter,
        ResetBehaviour,
        Count,
    }

    void Awake()
    {
        fsmRegiserZone = new FSM((int)RegisterZone_STATES.Count, (int)RegisterZone_EVENTS.Count, (int)RegisterZone_STATES.Idle);

        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.Idle, (int)RegisterZone_STATES.FinderRangePerimeter, (int)RegisterZone_EVENTS.StartBehaviour);

        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.FinderRangePerimeter, (int)RegisterZone_STATES.AssignedPositionMovement, (int)RegisterZone_EVENTS.DoneFinderRangePerimeter);
        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.FinderRangePerimeter, (int)RegisterZone_STATES.Idle, (int)RegisterZone_EVENTS.ResetBehaviour);

        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.AssignedPositionMovement, (int)RegisterZone_STATES.GoToRandomPosition, (int)RegisterZone_EVENTS.AssignedRandomPosition);
        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.AssignedPositionMovement, (int)RegisterZone_STATES.GoToSpecificPosition, (int)RegisterZone_EVENTS.AssignedSpecificPosition);
        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.AssignedPositionMovement, (int)RegisterZone_STATES.Idle, (int)RegisterZone_EVENTS.ResetBehaviour);

        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.GoToSpecificPosition, (int)RegisterZone_STATES.Wait, (int)RegisterZone_EVENTS.ArrivedPosition);
        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.GoToSpecificPosition, (int)RegisterZone_STATES.Idle, (int)RegisterZone_EVENTS.ResetBehaviour);

        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.GoToRandomPosition, (int)RegisterZone_STATES.Wait, (int)RegisterZone_EVENTS.ArrivedPosition);
        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.GoToRandomPosition, (int)RegisterZone_STATES.Idle, (int)RegisterZone_EVENTS.ResetBehaviour);

        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.Wait, (int)RegisterZone_STATES.FinderRangePerimeter, (int)RegisterZone_EVENTS.OnFinderRangePerimeter);
        fsmRegiserZone.SetRelations((int)RegisterZone_STATES.Wait, (int)RegisterZone_STATES.Idle, (int)RegisterZone_EVENTS.ResetBehaviour);
    }

    void Start()
    {
        auxDelayWaitRegister = delayWaitRegister;
    }

    void OnEnable()
    {
        FildOfView.OnViewTargetWhitFildOfViewCheck += CheckFindTarget;
    }

    void OnDisable()
    {
        FildOfView.OnViewTargetWhitFildOfViewCheck -= CheckFindTarget;
    }

    public void DisableRegisterObject()
    {
        registerObject.SetActive(false);
    }

    public void EnableRegisterObject()
    {
        registerObject.SetActive(true);
    }

    public void CheckFindTarget(Transform _transform, FildOfView _fildOfView)
    {
        if (_fildOfView != fildOfView)
            return;

        for (int i = 0; i < targetsTag.Count; i++)
        {
            if (targetsTag[i] == _transform.tag)
            {
                foundTarget = true;
                i = targetsTag.Count;
            }
        }
    }

    public void UpdateRegisterZone()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log((RegisterZone_STATES)fsmRegiserZone.GetCurrentState());
            Debug.Log(currentTarget);
        }

        fildOfView.FindVisibleTargets();

        switch (fsmRegiserZone.GetCurrentState())
        {
            case (int)RegisterZone_STATES.Idle:
                Idle();
                break;
            case (int)RegisterZone_STATES.FinderRangePerimeter:
                FinderRangePerimeter();
                break;
            case (int)RegisterZone_STATES.AssignedPositionMovement:
                AssignedPositionMovement();
                break;
            case (int)RegisterZone_STATES.GoToSpecificPosition:
                GoToSpecificPosition();
                break;
            case (int)RegisterZone_STATES.GoToRandomPosition:
                GoToRandomPosition();
                break;
            case (int)RegisterZone_STATES.Wait:
                Wait();
                break;
        }
    }

    void Idle()
    {
        registerObject.SetActive(false);

        if (enableRegisterZone)
        {
            fsmRegiserZone.SendEvent((int)RegisterZone_EVENTS.StartBehaviour);
        }
    }

    void FinderRangePerimeter()
    {
        registerObject.SetActive(true);
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        specificsTargetsPositions.Clear();

        float distanceForward = DistanceRayCasterZone(Vector3.forward);
        float distanceBack = DistanceRayCasterZone(Vector3.back);
        float distanceRight = DistanceRayCasterZone(Vector3.right);
        float distanceLeft = DistanceRayCasterZone(Vector3.left);

        float minX = GeneratorPerimeterRegisterObject.transform.position.x - distanceLeft;
        float maxX = GeneratorPerimeterRegisterObject.transform.position.x + distanceRight;

        float minZ = GeneratorPerimeterRegisterObject.transform.position.z - distanceBack;
        float maxZ = GeneratorPerimeterRegisterObject.transform.position.z + distanceForward;

        float x = minX + maxX;
        float centerX = x / 2;

        float y = transform.position.y;

        float z = minZ + maxZ;
        float centerZ = z / 2;

        float scalX = Vector3.Distance(new Vector3(minX, 0, 0), new Vector3(maxX, 0, 0));
        float scalY = myPrincipalObject.transform.localScale.y;
        float scalZ = Vector3.Distance(new Vector3(0, 0, minZ), new Vector3(0, 0, maxZ));

        Vector3 center = new Vector3(centerX, y, centerZ);
        Vector3 halfExtended = new Vector3(scalX, scalY, scalZ);

        centerGizmos = center;
        halfExtendedGizmos = halfExtended;

        Collider[] colliders = Physics.OverlapBox(center, halfExtended, Quaternion.identity, findObjectLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = 0; j < targetsSpecificsWaypointsTags.Count; j++) 
            {
                if (targetsSpecificsWaypointsTags[j] == colliders[i].tag)
                {
                    specificsTargetsPositions.Add(colliders[i].transform.position);
                }
            }
        }

       
        randomTargetPosition = new Vector3( Random.Range(minX, maxX)
                                           , y
                                           , Random.Range(minZ, maxZ));

        fsmRegiserZone.SendEvent((int)RegisterZone_EVENTS.DoneFinderRangePerimeter);

        CheckEnableRegiserZone();
    }

    void AssignedPositionMovement()
    {
        registerObject.SetActive(true);
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        float porcentageResult = Random.Range(0.0f, 100.0f);

        RegisterZone_EVENTS sendCurrentEvent = RegisterZone_EVENTS.Count;

        if (porcentageResult <= porcentageRandomTarget || specificsTargetsPositions.Count <= 0)
        {
            currentTarget = randomTargetPosition;

            sendCurrentEvent = RegisterZone_EVENTS.AssignedRandomPosition;

            fsmRegiserZone.SendEvent((int)sendCurrentEvent);
        }
        else
        {
            int indexTarget = Random.Range(0, specificsTargetsPositions.Count);

            currentTarget = new Vector3(specificsTargetsPositions[indexTarget].x, myPrincipalObject.transform.position.y, specificsTargetsPositions[indexTarget].z);

            sendCurrentEvent = RegisterZone_EVENTS.AssignedSpecificPosition;

            fsmRegiserZone.SendEvent((int)sendCurrentEvent);
        }

        CheckEnableRegiserZone();
    }

    void GoToSpecificPosition()
    {
        registerObject.SetActive(true);
        MovementWhitNavMesh(currentTarget);
        if (CheckArrivedTarget())
        {
            fsmRegiserZone.SendEvent((int)RegisterZone_EVENTS.ArrivedPosition);
            OnArrivedSpecificTarget?.Invoke();
        }

        CheckEnableRegiserZone();
    }

    void GoToRandomPosition()
    {
        registerObject.SetActive(true);
        MovementWhitNavMesh(currentTarget);
        if (CheckArrivedTarget())
            fsmRegiserZone.SendEvent((int)RegisterZone_EVENTS.ArrivedPosition);

        CheckEnableRegiserZone();
    }

    void Wait()
    {
        registerObject.SetActive(true);
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        if (delayWaitRegister > 0)
        {
            delayWaitRegister = delayWaitRegister - Time.deltaTime;
        }
        else
        {
            delayWaitRegister = auxDelayWaitRegister;
            fsmRegiserZone.SendEvent((int)RegisterZone_EVENTS.OnFinderRangePerimeter);
        }

        CheckEnableRegiserZone();
    }

    bool CheckArrivedTarget()
    {
        float distanceToTarget = Vector3.Distance(myPrincipalObject.transform.position, currentTarget);

        if (distanceToTarget <= distanceOfCurrentTarget)
        {
            return true;
        }

        return false;
    }

    float DistanceRayCasterZone(Vector3 direction)
    {
        RaycastHit hit;

        bool isHit = Physics.Raycast(GeneratorPerimeterRegisterObject.transform.position, direction, out hit, maxRangePerimeterRegister, layerWalls);

        float distance = maxRangePerimeterRegister;

        if (isHit)
        {
            //Debug.Log("Le di al hit uwu");
            distance = Vector3.Distance(GeneratorPerimeterRegisterObject.transform.position, hit.point);
        }

        return distance;
    }

    void MovementWhitNavMesh(Vector3 target)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedMovementRegister;
        navMeshAgent.acceleration = speedMovementRegister * 2;
        navMeshAgent.SetDestination(target);
    }

    void CheckEnableRegiserZone()
    {
        if (!enableRegisterZone)
        {
            fsmRegiserZone.SendEvent((int)RegisterZone_EVENTS.ResetBehaviour);
        }
    }
    public void SetEnableRegisterZone(bool value) => enableRegisterZone = value;

    public bool GetEnableRegisterZone() { return enableRegisterZone; }

    public void ResetFoundTarget() => foundTarget = false;

    public bool GetFoundTarget() { return foundTarget; }

    public void SetGeneraterPerimeterRegisterObject(GameObject _generatorPerimeterRegisterObject) => GeneratorPerimeterRegisterObject = _generatorPerimeterRegisterObject;

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(centerGizmos, halfExtendedGizmos);
    }
}
