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
    }

    public override void OnExit()
    {
        Debug.Log("DieState - OnExit");
    }
}