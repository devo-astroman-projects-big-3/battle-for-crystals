using Fusion;
using UnityEngine;
using System;

public class StateNetworkE1 : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnFsmStateChanged))]
    private int FsmState { get; set; }
    public event Action<int> FsmChangedAction;



    [Networked, OnChangedRender(nameof(OnLookAtStateChanged))]
    private Vector3 LookAtState { get; set; }
    public event Action<Vector3> LookAtChangedAction;

    public override void Spawned()
    {
        FsmChangedAction?.Invoke(FsmState);
    }

    public void ChangeFsmState(int newState)
    {
        if (Object == null || !Object.HasStateAuthority)
            return;

        FsmState = newState;
    }

    private void OnFsmStateChanged()
    {
        FsmChangedAction?.Invoke(FsmState);
    }

    public void ChangeLookAtState(Vector3 newLookAt)
    {
        if (Object == null || !Object.HasStateAuthority)
            return;

        LookAtState = newLookAt;
    }

    private void OnLookAtStateChanged()
    {
        LookAtChangedAction?.Invoke(LookAtState);
    }

    
}