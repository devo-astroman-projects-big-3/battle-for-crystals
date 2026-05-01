using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class HeroBornState : AbstractState
{
    private DepsHeroFSM _dependencies;

    public void Setup(ref DepsHeroFSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("BornState - OnEnter");
        _dependencies.mainHero.PlaceAtCheckpoint();
        _dependencies.mainHero.BlockInputs();
        _dependencies.bornToMoveTimeout.Reset();
        _dependencies.mainHero.EnableCollider();

        _dependencies.bornToMoveTimeout.SetTimeout(() =>
        {
            _dependencies.fsm.GoMoveNetwork();
        }, 2f);
    }

    public override void OnExit()
    {
        Debug.Log("BornState - OnExit");
        _dependencies.mainHero.UnblockInputs();
        _dependencies.bornToMoveTimeout.Pause();
    }
}
