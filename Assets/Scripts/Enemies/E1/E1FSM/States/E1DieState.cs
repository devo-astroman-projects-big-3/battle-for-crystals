using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class E1DieState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("DieState - OnEnter");
        _dependencies.prerestartTimeout.Reset();

        _dependencies.e1.HideSkin();
        _dependencies.prerestartTimeout.SetTimeout(() =>
        {
            _dependencies.fsm.ResetHitNetwork();
        },5f);
    }

    public override void OnExit()
    {
        _dependencies.prerestartTimeout.Pause();
    }
}