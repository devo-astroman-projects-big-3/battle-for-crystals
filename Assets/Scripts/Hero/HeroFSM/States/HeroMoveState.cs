using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class HeroMoveState : AbstractState
{
    private DepsHeroFSM _dependencies;

    public void Setup(ref DepsHeroFSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("MoveState - OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("MoveState - OnExit");
    }
}
