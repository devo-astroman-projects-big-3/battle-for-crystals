using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class E1RestartState : AbstractState
{
    private DepsE1FSM _dependencies;

    public void Setup(ref DepsE1FSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        /* Debug.Log("RestartState - OnEnter"); */
        _dependencies.extraTimeout.Reset();

        _dependencies.e1.HideEnemy();
        _dependencies.extraTimeout.SetTimeout(() =>
        {
            _dependencies.fsm.GoHideNetwork();
        },2f);
    }

    public override void OnExit()
    {
        /* Debug.Log("RestartState - OnExit"); */
        _dependencies.e1.ShowSkin();
        _dependencies.extraTimeout.Pause();
    }
}