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
        //_dependencies.e1.Fire();

        /* 
            Should fire and on detect when the animation has finished and should go back to show
        
         */
    }

    public override void OnUpdate()
    {
        // When key "1" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Key 1 pressed → GoShow");
            _dependencies.fsm.GoShow();
        }
    }

    public override void OnExit()
    {
        Debug.Log("AttackState - OnExit");
    }
}