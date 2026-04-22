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
        Debug.Log("HideState - OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("HideState - OnExit");
    }
}