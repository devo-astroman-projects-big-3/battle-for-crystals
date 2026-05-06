using KevinCastejon.FiniteStateMachine;
using UnityEngine;

public class DepsE1FSM
{    
    public int id;
    public E1 e1;
    public E1FSM fsm;
    public SetTimeoutUtility preattackTimeout;
    public SetTimeoutUtility prerestartTimeout;
    public SetTimeoutUtility extraTimeout;
}

public class E1FSM : AbstractFiniteStateMachine
{
    [SerializeField] private E1 _e1;
    [SerializeField] private StateNetworkE1 _stateNetworkE1;

    public enum E1States
    {
        HIDE_STATE,
        SHOW_STATE,
        ATTACK_STATE,
        RECEIVE_HIT_STATE,
        DIE_STATE,
        RESTART_STATE,
    }

    public DepsE1FSM deps = new DepsE1FSM
    {
        id = 0,
        e1 = null,
        fsm = null,
        preattackTimeout = null,
        prerestartTimeout = null,
        extraTimeout = null,
    };

    private void Awake()
    {
        deps.id = 0;
        deps.e1 = _e1;
        deps.fsm = this;
        deps.preattackTimeout = new SetTimeoutUtility(this);
        deps.prerestartTimeout = new SetTimeoutUtility(this);
        deps.extraTimeout = new SetTimeoutUtility(this);
        

        E1HideState e1Hide = AbstractState.Create<E1HideState, E1States>(E1States.HIDE_STATE, this);
        e1Hide.Setup(ref deps);

        E1ShowState e1Show = AbstractState.Create<E1ShowState, E1States>(E1States.SHOW_STATE, this);
        e1Show.Setup(ref deps);

        E1AttackState e1Attack = AbstractState.Create<E1AttackState, E1States>(E1States.ATTACK_STATE, this);
        e1Attack.Setup(ref deps);

        E1ReceiveHitState e1ReceiveHit = AbstractState.Create<E1ReceiveHitState, E1States>(E1States.RECEIVE_HIT_STATE, this);
        e1ReceiveHit.Setup(ref deps);

        E1DieState e1Die = AbstractState.Create<E1DieState, E1States>(E1States.DIE_STATE, this);
        e1Die.Setup(ref deps);

        E1RestartState e1Restart = AbstractState.Create<E1RestartState, E1States>(E1States.RESTART_STATE, this);
        e1Restart.Setup(ref deps);

        Init(E1States.HIDE_STATE, e1Hide, e1Show, e1Attack, e1ReceiveHit, e1Die,e1Restart);

        _stateNetworkE1.FsmChangedAction += HandleFsmChanged;
        _stateNetworkE1.LookAtChangedAction += HandleLookAtChanged;
        _stateNetworkE1.NHitsReceivedChangedAction += HandleNHitsReceivedChanged;

    }

    private void OnDestroy()
    {
        _stateNetworkE1.FsmChangedAction -= HandleFsmChanged;
        _stateNetworkE1.LookAtChangedAction -= HandleLookAtChanged;
        _stateNetworkE1.NHitsReceivedChangedAction -= HandleNHitsReceivedChanged;
    }

    public void GoShow()
    {
        TransitionToState(E1States.SHOW_STATE);
    }

    public void GoAttack()
    {
        TransitionToState(E1States.ATTACK_STATE);
    }

    public void GoReceiveHit()
    {
        TransitionToState(E1States.RECEIVE_HIT_STATE);
    }

    public void GoHide()
    {
        TransitionToState(E1States.HIDE_STATE);
    }

    public void GoDie()
    {
        TransitionToState(E1States.DIE_STATE);
    }

    public void GoRestart()
    {
        TransitionToState(E1States.RESTART_STATE);
    }

    public void GoShowNetwork()
    {
        _stateNetworkE1.ChangeFsmState(1);
    }

    public void GoAttackNetwork()
    {
        _stateNetworkE1.ChangeFsmState(2);
    }

    public void GoReceiveHitNetwork()
    {
        _stateNetworkE1.ChangeFsmState(3);
    }

    public void GoHideNetwork()
    {
        _stateNetworkE1.ChangeFsmState(0);
    }

    public void GoDieNetwork()
    {
        _stateNetworkE1.ChangeFsmState(4);
    }

    public void GoRestartNetwork()
    {
        _stateNetworkE1.ChangeFsmState(5);
    }

    public void SetLookAtNetwork(Vector3 lookAtPos)
    {
        _stateNetworkE1.ChangeLookAtState(lookAtPos);
    }

    public void NotifyReceiveHitNetwork()
    {
        _stateNetworkE1.RequestReceiveHit();
    }

    public void ResetHitNetwork()
    {
        _stateNetworkE1.RequestResetNHits();
    }

    public int GetNHitsReceived()
    {
        return _stateNetworkE1.GetNHitsReceived();        
    }

    private void HandleFsmChanged(int newState)
    {
        if(newState == 0)
        {
            GoHide();

        }else if (newState == 1)
        {
            GoShow();

        }else if (newState == 2)
        {
            GoAttack();

        }else if (newState == 3)
        {
            GoReceiveHit();

        }else if (newState == 4)
        {
            GoDie();
        
        }else if (newState == 5)
        {
            GoRestart();
        }
    }

    private void HandleLookAtChanged(Vector3 lookAtPos)
    {
        _e1.SetLookAtPos(lookAtPos);
        _e1.LookAtTarget();
    }

    private void HandleNHitsReceivedChanged(int nHits)
    {   
        if(nHits > 0)
        {
            GoReceiveHitNetwork();
        }
        else
        {
            GoRestartNetwork();
        }
    }
    
}
