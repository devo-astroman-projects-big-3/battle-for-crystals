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
    }

    public override void OnExit()
    {
        Debug.Log("BornState - OnExit");
    }
}
