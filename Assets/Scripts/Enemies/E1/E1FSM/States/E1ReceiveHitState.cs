using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class E1ReceiveHitState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("ReceiveState - OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("ReceiveState - OnExit");
    }
}