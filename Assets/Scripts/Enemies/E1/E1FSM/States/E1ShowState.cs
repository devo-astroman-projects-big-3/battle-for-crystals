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
        Debug.Log("ShowState - OnEnter");
        //_dependencies.e1.ShowEnemy();
    }

    public override void OnUpdate()
    {
        // When key "1" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Key 1 pressed → GoAttack");
            _dependencies.e1.Fire();
            _dependencies.fsm.GoAttack();
        }

        // When key "2" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Key 2 pressed → GoHide");
            _dependencies.fsm.GoHide();
        }

        // When key "3" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Key 3 pressed → GoReceiveHit");
            _dependencies.e1.ReceiveDamage();
            _dependencies.fsm.GoReceiveHit();
        }

        // When key "4" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Key 4 pressed → GoDie");
            _dependencies.fsm.GoDie();
        }
    }

    public override void OnExit()
    {
        Debug.Log("ShowState - OnExit");
    }
}