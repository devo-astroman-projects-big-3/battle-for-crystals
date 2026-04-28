using KevinCastejon.FiniteStateMachine;
using UnityEngine;

public class DepsHeroFSM
{    
    public int id;
    public MainHero mainHero;
    public HeroFSM fsm;
    public SetTimeoutUtility bornToMoveTimeout;
}

public class HeroFSM : AbstractFiniteStateMachine
{
    [SerializeField] private MainHero _mainHero;
    //[SerializeField] private StateNetworkE1 _stateNetworkE1;

    public enum HeroStates
    {
        BORN_STATE,
        MOVE_STATE,
        RECEIVE_HIT_STATE,
        DIE_STATE,
    }

    public DepsHeroFSM deps = new DepsHeroFSM
    {
        id = 0,
        mainHero = null,
        fsm = null,
        bornToMoveTimeout = null,
    };

    private void Awake()
    {
        deps.id = 0;
        deps.mainHero = _mainHero;
        deps.fsm = this;
        deps.bornToMoveTimeout = new SetTimeoutUtility(this);

        HeroBornState heroBorn = AbstractState.Create<HeroBornState, HeroStates>(HeroStates.BORN_STATE, this);
        heroBorn.Setup(ref deps);

        HeroMoveState heroMove = AbstractState.Create<HeroMoveState, HeroStates>(HeroStates.MOVE_STATE, this);
        heroMove.Setup(ref deps);

        HeroReceiveHitState heroReceiveHit = AbstractState.Create<HeroReceiveHitState, HeroStates>(HeroStates.RECEIVE_HIT_STATE, this);
        heroReceiveHit.Setup(ref deps);

        HeroDieState heroDie = AbstractState.Create<HeroDieState, HeroStates>(HeroStates.DIE_STATE, this);
        heroDie.Setup(ref deps);

        Init(HeroStates.BORN_STATE, heroBorn, heroMove, heroReceiveHit, heroDie);
    }

    private void OnDestroy()
    {
    }


    

    
}
