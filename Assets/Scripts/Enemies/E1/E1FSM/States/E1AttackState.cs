using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class E1AttackState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("AttackState - OnEnter");

        _dependencies.e1.MiddleFireEventAction += HandleMiddleFireEvent;
        _dependencies.e1.FireAnimationEndsAction += HandleFireAnimationEnds;        
        _dependencies.e1.PlayerUndetectedAction += HandlePlayerUndetected;
        _dependencies.e1.ReceiveHitAction += HandleReceiveHit;
        _dependencies.e1.Fire();
    }

    public override void OnExit()
    {
        _dependencies.e1.MiddleFireEventAction -= HandleMiddleFireEvent;
        _dependencies.e1.FireAnimationEndsAction -= HandleFireAnimationEnds;        
        _dependencies.e1.PlayerUndetectedAction -= HandlePlayerUndetected;
        _dependencies.e1.ReceiveHitAction -= HandleReceiveHit;
    }

    private void HandleMiddleFireEvent()
    {
        _dependencies.e1.FireBullet();
    }
    private void HandleFireAnimationEnds()
    {
        //_dependencies.fsm.GoShow();
        _dependencies.fsm.GoShowNetwork();
    }

    private void HandleReceiveHitAnimationEnds()
    {
        //_dependencies.fsm.GoReceiveHit();
        _dependencies.fsm.GoReceiveHitNetwork();
    }

    private void HandlePlayerUndetected()
    {
        _dependencies.e1.SetTarget(null);
    }

    private void HandleReceiveHit(int amount)
    {
        //_dependencies.e1.ReceiveDamage(amount);
        //check life 
        //life > 0
        //_dependencies.fsm.GoReceiveHit();
        //_dependencies.fsm.GoReceiveHitNetwork();
        
        //_dependencies.e1.ReceiveDamage();
        _dependencies.fsm.NotifyReceiveHitNetwork();
        
        //life == 0
        //_dependencies.fsm.GoDie();
    }
}