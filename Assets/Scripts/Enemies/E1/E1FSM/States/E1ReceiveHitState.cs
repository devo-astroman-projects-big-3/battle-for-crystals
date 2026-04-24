using UnityEngine;
using KevinCastejon.FiniteStateMachine;
using Unity.VisualScripting;
public class E1ReceiveHitState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        /* Debug.Log("ReceiveState - OnEnter"); */
        _dependencies.e1.ReceiveHitAnimationEndsAction += HandleReceiveHitAnimationEnds;        
    }


    public override void OnExit()
    {
        /* Debug.Log("ReceiveState - OnExit"); */
        _dependencies.e1.ReceiveHitAnimationEndsAction -= HandleReceiveHitAnimationEnds;
    }

    private void HandleReceiveHitAnimationEnds()
    {
        if(_dependencies.e1.GetLife() > 0)
        {
            //_dependencies.fsm.GoShow();
            _dependencies.fsm.GoShowNetwork();
        }
        else
        {
            _dependencies.e1.Die();
            //_dependencies.fsm.GoDie();
            _dependencies.fsm.GoDieNetwork();
        }
        

    }
}