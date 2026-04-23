using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class E1HideState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        /* Debug.Log("HideState - OnEnter"); */
        _dependencies.e1.PlayerDetectedAction += HandlePlayerDetected;
        

    }

    public override void OnExit()
    {
        /* Debug.Log("HideState - OnExit"); */
        _dependencies.e1.PlayerDetectedAction -= HandlePlayerDetected;
    }

    private void HandlePlayerDetected(GameObject playerDetected)
    {
        _dependencies.e1.ShowEnemy();
        _dependencies.e1.SetTarget(playerDetected.transform);
        _dependencies.fsm.GoShow();

    }
}