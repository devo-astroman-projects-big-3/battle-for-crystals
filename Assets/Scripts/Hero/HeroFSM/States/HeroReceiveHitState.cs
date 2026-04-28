using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class HeroReceiveHitState : AbstractState
{
    private DepsHeroFSM _dependencies;

    public void Setup(ref DepsHeroFSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("ReceiveHitState - OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("ReceiveHitState - OnExit");
    }
}
