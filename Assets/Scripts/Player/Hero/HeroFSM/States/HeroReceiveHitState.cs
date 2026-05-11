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
        /* Debug.Log("////// ReceiveHitState - OnEnter"); */
        _dependencies.mainHero.BlockInputs();

        _dependencies.mainHero.ReceiveHitAnimationEndsAction += HandleReceiveHitAnimationEnds;
        _dependencies.mainHero.PlayReceiveHit();
        _dependencies.mainHero.ShowHitFlickerGlow();
        _dependencies.fsm.NotifyReceiveHitNetwork();
        _dependencies.mainHero.DisableCollider();

    }

    public override void OnExit()
    {
        /* Debug.Log("ReceiveHitState - OnExit"); */
        _dependencies.mainHero.ReceiveHitAnimationEndsAction -= HandleReceiveHitAnimationEnds;
        _dependencies.mainHero.UnblockInputs();
        _dependencies.mainHero.StopHitFlickerGlow();
    }

    private void HandleReceiveHitAnimationEnds()
    {

        if (_dependencies.fsm.IsDead())
        {
            _dependencies.fsm.GoDieNetwork();
        }
        else
        {
            _dependencies.fsm.GoMoveNetwork();
        }

        //NHitsReceivedChangedAction
        
    }
}
