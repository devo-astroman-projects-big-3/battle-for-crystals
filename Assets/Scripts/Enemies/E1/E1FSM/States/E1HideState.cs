using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class E1HideState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("HideState - OnEnter");
        //_dependencies.e1.HideEnemy();

    }

    public override void OnUpdate()
    {
        // When key "1" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Key 1 pressed → GoShow");
            _dependencies.e1.ShowEnemy();
            _dependencies.fsm.GoShow();
        }
    }

    public override void OnExit()
    {
        Debug.Log("HideState - OnExit");
    }
}