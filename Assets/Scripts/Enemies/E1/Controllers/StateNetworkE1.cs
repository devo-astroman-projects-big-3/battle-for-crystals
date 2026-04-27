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


    [Networked, OnChangedRender(nameof(OnNHitsReceivedChanged))] private int NHitsReceived { get; set; }
    public event Action<int> NHitsReceivedChangedAction;


    public override void Spawned()
    {
        FsmChangedAction?.Invoke(FsmState);
        NHitsReceived = 0;
    }

    public void ChangeFsmState(int newState)
    {
        if (Object == null || !Object.HasStateAuthority)            
            return;

        FsmState = newState;
    }

    public int GetNHitsReceived()
    {
        return NHitsReceived;
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


    public void RequestReceiveHit()
    {
        if (Object == null)
        {
            Debug.LogWarning("E1 is not spawned yet.");
            return;
        }

        RPC_ReceiveHit();
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_ReceiveHit()
    {
        NHitsReceived++;
    }

    public void RequestResetNHits()
    {
        if (Object == null)
        {
            Debug.LogWarning("E1 is not spawned yet.");
            return;
        }

        RPC_ResetNHits();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_ResetNHits()
    {
        NHitsReceived = 0;
    }
    
    private void OnNHitsReceivedChanged()
    {
        NHitsReceivedChangedAction?.Invoke(NHitsReceived);
    }

    
}