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
    [SerializeField] private HeroStateNetwork _heroStateNetwork;

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

        _heroStateNetwork.FsmChangedAction += HandleFsmChanged;

    }

    private void OnDestroy()
    {
        _heroStateNetwork.FsmChangedAction -= HandleFsmChanged;
    }

    public void GoMove()
    {
        //_stateNetworkE1.RequestChangeFsm(1);
        TransitionToState(HeroStates.MOVE_STATE);
    }

    public void GoReceiveHit()
    {
        //_stateNetworkE1.RequestChangeFsm(2);
        TransitionToState(HeroStates.RECEIVE_HIT_STATE);
    }

    public void GoDie()
    {
        //_stateNetworkE1.RequestChangeFsm(3);
        TransitionToState(HeroStates.DIE_STATE);
    }

    public void GoBorn()
    {
        //_stateNetworkE1.RequestChangeFsm(0);
        TransitionToState(HeroStates.BORN_STATE);
    }

    //Network calls
    public void GoMoveNetwork()
    {
        _heroStateNetwork.ChangeFsmState(1);
        //TransitionToState(_heroStateNetwork.MOVE_STATE);
    }

    public void GoReceiveHitNetwork()
    {
        _heroStateNetwork.ChangeFsmState(2);
        //TransitionToState(_heroStateNetwork.RECEIVE_HIT_STATE);
    }

    public void GoDieNetwork()
    {
        _heroStateNetwork.ChangeFsmState(3);
        //TransitionToState(_heroStateNetwork.DIE_STATE);
    }

    public void GoBornNetwork()
    {
        _heroStateNetwork.ChangeFsmState(0);
        //TransitionToState(_heroStateNetwork.BORN_STATE);
    }

    private void HandleFsmChanged(int newState)
    {
        if(newState == 0)
        {
            GoBorn();

        }else if (newState == 1)
        {
            GoMove();

        }else if (newState == 2)
        {
            GoReceiveHit();

        }else if (newState == 3)
        {
            GoDie();
        }
    }
    
}
