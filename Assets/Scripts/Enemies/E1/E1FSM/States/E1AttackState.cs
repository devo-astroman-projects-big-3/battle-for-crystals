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
        /* Debug.Log("AttackState - OnEnter"); */

        _dependencies.e1.MiddleFireEventAction += HandleMiddleFireEvent;
        _dependencies.e1.FireAnimationEndsAction += HandleFireAnimationEnds;        
        _dependencies.e1.PlayerUndetectedAction += HandlePlayerUndetected;
        _dependencies.e1.ReceiveHitAction += HandleReceiveHit;
        _dependencies.e1.Fire();

        /* 
            Should fire and on detect when the animation has finished and should go back to show
        
         */
    }

    public override void OnExit()
    {
        /* Debug.Log("AttackState - OnExit"); */
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
        _dependencies.fsm.GoShow();
    }

    private void HandleReceiveHitAnimationEnds()
    {
        _dependencies.fsm.GoReceiveHit();
    }

    private void HandlePlayerUndetected()
    {
        _dependencies.e1.SetTarget(null);     
    }

    private void HandleReceiveHit(int amount)
    {
        _dependencies.e1.ReceiveDamage(amount);
        //check life 
        //life > 0
        _dependencies.fsm.GoReceiveHit();
        
        //life == 0
        //_dependencies.fsm.GoDie();
    }
}