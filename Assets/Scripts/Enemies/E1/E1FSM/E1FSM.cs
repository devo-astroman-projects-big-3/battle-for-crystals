using KevinCastejon.FiniteStateMachine;
using UnityEngine;

public class DepsE1FSM
{    
    public int id;
    public E1 e1;
    public E1FSM fsm;    
}

public class E1FSM : AbstractFiniteStateMachine
{
    [SerializeField] private E1 _e1;

    public enum E1States
    {
        HIDE_STATE,
        SHOW_STATE,
        ATTACK_STATE,
        RECEIVE_HIT_STATE,
        DIE_STATE,
    }

    public DepsE1FSM deps = new DepsE1FSM
    {
        id = 0,                
        e1 = null,
        fsm = null,
    };

    private void Awake()
    {
        deps.id = 0;
        deps.e1 = _e1;
        deps.fsm = this;

        E1HideState e1Hide = AbstractState.Create<E1HideState, E1States>(E1States.HIDE_STATE, this);
        e1Hide.Setup(ref deps);

        E1ShowState e1Show = AbstractState.Create<E1ShowState, E1States>(E1States.SHOW_STATE, this);
        e1Show.Setup(ref deps);

        E1AttackState e1Attack = AbstractState.Create<E1AttackState, E1States>(E1States.ATTACK_STATE, this);
        e1Attack.Setup(ref deps);

        E1ReceiveHitState e1ReceiveHit = AbstractState.Create<E1ReceiveHitState, E1States>(E1States.RECEIVE_HIT_STATE, this);
        e1ReceiveHit.Setup(ref deps);

        E1DieState e1Die = AbstractState.Create<E1DieState, E1States>(E1States.RECEIVE_HIT_STATE, this);
        e1Die.Setup(ref deps);

        Init(E1States.HIDE_STATE, e1Hide, e1Show, e1Attack, e1ReceiveHit, e1Die);
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
}