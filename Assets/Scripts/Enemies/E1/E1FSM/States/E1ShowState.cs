using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class E1ShowState : AbstractState
{
    private DepsE1FSM _dependencies;
    private bool _ignoreTime = true;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        /* Debug.Log("ShowState - OnEnter"); */
        _dependencies.preattackTimeout.Reset();
        _dependencies.e1.PlayerUndetectedAction += HandlePlayerUndetected;
        _dependencies.e1.ReceiveHitAction += HandleReceiveHit;

        _dependencies.preattackTimeout.SetTimeout(() =>
        {
         
            _dependencies.fsm.GoAttack();

        },1f);

        if (_dependencies.e1.IsTargetNull())
        {
            _dependencies.e1.HideEnemy();
            _dependencies.fsm.GoHide();
        }

        _dependencies.e1.LookAtTarget();
    }

    public override void OnUpdate()
    {
        // When key "1" is pressed
        //_dependencies.e1.LookAtTarget();
    }


    public override void OnExit()
    {
        /* Debug.Log("ShowState - OnExit"); */
        _dependencies.e1.PlayerUndetectedAction -= HandlePlayerUndetected;
        _dependencies.e1.ReceiveHitAction -= HandleReceiveHit;
        _dependencies.preattackTimeout.Pause();
    }

    private void HandlePlayerUndetected()
    {
        /* Debug.Log("GO HIDE"); */
        _dependencies.e1.HideEnemy();
        _dependencies.e1.SetTarget(null);
        _dependencies.fsm.GoHide();
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