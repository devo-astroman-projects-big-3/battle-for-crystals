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
        _dependencies.mainHero.BlockInputs();
        _dependencies.bornToMoveTimeout.Reset();

        _dependencies.bornToMoveTimeout.SetTimeout(() =>
        {
            //_dependencies.fsm.GoMoveNetwork();
            Debug.Log("Should go to move");
        }, 2f);
    }

    public override void OnExit()
    {
        Debug.Log("BornState - OnExit");
        _dependencies.mainHero.UnblockInputs();
        _dependencies.bornToMoveTimeout.Pause();
    }
}
