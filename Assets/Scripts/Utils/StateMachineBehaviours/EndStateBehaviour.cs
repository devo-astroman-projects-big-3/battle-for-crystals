using UnityEngine;
using System;

public class EndStateBehaviour : StateMachineBehaviour
{
    public string stateName;
    public event Action<string> StateExitAction;

    public override void OnStateExit(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        StateExitAction?.Invoke(stateName);
    }
}