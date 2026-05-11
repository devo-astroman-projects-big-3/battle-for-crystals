using Fusion;
using UnityEngine;
using System;

public class HeroStateNetwork : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnFsmStateChanged))]
    private int FsmState { get; set; }
    public event Action<int> FsmChangedAction;


    [Networked, OnChangedRender(nameof(OnNHitsReceivedChanged))] private int NHitsReceived { get; set; }
    public event Action<int> NHitsReceivedChangedAction;


    public override void Spawned()
    {
        FsmChangedAction?.Invoke(FsmState);
        NHitsReceived = 0;
    }

    public void RequestChangeFsmState(int newState)
    {
        if (Object == null)
        {
            Debug.Log("FLAG0");
            return;
        }

        RPC_ChangeFsmState(newState);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_ChangeFsmState(int newState)
    {
        //Debug.Log($"State changed on authority {Object.InputAuthority}");

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


    public void RequestReceiveHit()
    {
        if (Object == null)
        {
            Debug.LogWarning("Hero is not spawned yet.");
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