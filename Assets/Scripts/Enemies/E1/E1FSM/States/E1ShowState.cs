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
        _dependencies.preattackTimeout.SetTimeout(() =>
        {
            _dependencies.fsm.GoAttack();        
        },1f);
    }

    public override void OnUpdate()
    {
        // When key "1" is pressed
        _dependencies.e1.LookAtTarget();
    }


    public override void OnExit()
    {
        Debug.Log("ShowState - OnExit");
    }
}