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
        /* Debug.Log("ShowState - OnEnter"); */
        _dependencies.preattackTimeout.Reset();
        _dependencies.e1.PlayerUndetectedAction += HandlePlayerUndetected;
        _dependencies.e1.ReceiveHitAction += HandleReceiveHit;

        if(!_dependencies.e1.IsTargetNull() && _dependencies.e1.GetDistance() > 22)
        {
            _dependencies.e1.SetTargetNull();
        }


        _dependencies.preattackTimeout.SetTimeout(() =>
        {
            if (!_dependencies.e1.IsTargetNull())
            {
                Transform playerTransform = _dependencies.e1.GetTarget();
                Vector3 lookAtPos = playerTransform.position;
                _dependencies.fsm.SetLookAtNetwork(lookAtPos);
                
                _dependencies.fsm.GoAttackNetwork();
            }
            else
            {
                 _dependencies.e1.HideEnemy();
                _dependencies.fsm.GoHideNetwork();
            }

        },1f);
        
        
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
        _dependencies.fsm.GoHideNetwork();

    }

    private void HandleReceiveHit(int amount)
    {    
        _dependencies.fsm.NotifyReceiveHitNetwork();
       
    }

    

}