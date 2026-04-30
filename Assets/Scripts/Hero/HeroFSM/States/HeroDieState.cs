using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class HeroDieState : AbstractState
{
    private DepsHeroFSM _dependencies;

    public void Setup(ref DepsHeroFSM deps)
    {
        _dependencies = deps;
    }

    public override void OnEnter()
    {
        Debug.Log("DieState - OnEnter");
        _dependencies.deadTimeout.Reset();

        //_dependencies.mainHero.BlockInputs();
        _dependencies.mainHero.ApplyDead();

        //_dependencies.h1.HideSkin();
        _dependencies.deadTimeout.SetTimeout(() =>
        {
            _dependencies.fsm.ResetHitNetwork();
            _dependencies.fsm.GoBorn();
        },5f);
    }

    public override void OnExit()
    {
        Debug.Log("DieState - OnExit");
        _dependencies.mainHero.UnblockInputs();
        _dependencies.deadTimeout.Pause();
    }
}