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
        Debug.Log("////// ReceiveHitState - OnEnter");

        _dependencies.mainHero.ReceiveHitAnimationEndsAction += HandleReceiveHitAnimationEnds;
        _dependencies.mainHero.PlayReceiveHit();

    }

    public override void OnExit()
    {
        Debug.Log("ReceiveHitState - OnExit");
        _dependencies.mainHero.ReceiveHitAnimationEndsAction -= HandleReceiveHitAnimationEnds;
    }

    private void HandleReceiveHitAnimationEnds()
    {
        _dependencies.fsm.GoMoveNetwork();
    }
}
