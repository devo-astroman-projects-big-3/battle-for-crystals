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
        /* Debug.Log("ReceiveHitState - OnEnter"); */
        _dependencies.e1.ReceiveHitAnimationEndsAction += HandleReceiveHitAnimationEnds;
        _dependencies.e1.ReceiveDamage();
        _dependencies.e1.ShowHitFlickerGlow();
    }


    public override void OnExit()
    {
        /* Debug.Log("ReceiveHitState - OnExit"); */
        _dependencies.e1.ReceiveHitAnimationEndsAction -= HandleReceiveHitAnimationEnds;
        _dependencies.e1.StopHitFlickerGlow();
    }

    private void HandleReceiveHitAnimationEnds()
    {        
        
        int nHitsReceived = _dependencies.fsm.GetNHitsReceived();
        if (_dependencies.e1.IsDead(nHitsReceived))
        {
            _dependencies.fsm.GoDieNetwork();
        }
        else
        {
            _dependencies.fsm.GoShowNetwork();
        }

    }
}