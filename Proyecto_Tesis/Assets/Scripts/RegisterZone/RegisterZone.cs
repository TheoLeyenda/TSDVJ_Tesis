using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RegisterZone : MonoBehaviour
{
    [SerializeField] private GameObject registerObject;
    [SerializeField] private List<string> targetsTag;
    [SerializeField] private FildOfView fildOfView;
    [SerializeField] private float speedMovementRegister;
    [SerializeField] private float maxRangePerimeterRegister;
    private bool foundTarget = false;
    private FSM fsmRegiserZone;

    private bool startRegisterZone = false;

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

    void OnEnable()
    {
        FildOfView.OnViewTarget += CheckFindTarget;
    }

    void OnDisable()
    {
        FildOfView.OnViewTarget -= CheckFindTarget;
    }

    public void CheckFindTarget(Transform _transform)
    {
        for (int i = 0; i < targetsTag.Count; i++)
        {
            if (targetsTag[i] == _transform.tag)
            {
                foundTarget = true;
                i = targetsTag.Count;
            }
        }
    }

    public void MovementRegisterZone(NavMeshAgent navMesh)
    {

    }

    public void ResetFoundTarget() => foundTarget = false;

    public bool GetFoundTarget() { return foundTarget; }
}
