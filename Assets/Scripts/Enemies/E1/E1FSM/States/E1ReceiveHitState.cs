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
        Debug.Log("ReceiveState - OnEnter");
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
        Debug.Log("ReceiveState - OnExit");
    }
}