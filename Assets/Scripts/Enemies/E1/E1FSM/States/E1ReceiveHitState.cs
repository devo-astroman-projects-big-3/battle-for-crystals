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
    }


    public override void OnExit()
    {
        /* Debug.Log("ReceiveHitState - OnExit"); */
        _dependencies.e1.ReceiveHitAnimationEndsAction -= HandleReceiveHitAnimationEnds;
    }

    private void HandleReceiveHitAnimationEnds()
    {        
        _dependencies.fsm.GoShowNetwork();

        /* if(_dependencies.e1.GetLife() > 0)
        {
            //_dependencies.fsm.GoShow();
            _dependencies.fsm.GoShowNetwork();
        }
        else
        {
            _dependencies.e1.Die();
            //_dependencies.fsm.GoDie();
            _dependencies.fsm.GoDieNetwork();
        } */
        

    }
}