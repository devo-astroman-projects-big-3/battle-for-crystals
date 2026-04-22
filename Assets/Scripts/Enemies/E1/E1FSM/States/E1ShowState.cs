using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class E1ShowState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("ShowState - OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("ShowState - OnExit");
    }
}