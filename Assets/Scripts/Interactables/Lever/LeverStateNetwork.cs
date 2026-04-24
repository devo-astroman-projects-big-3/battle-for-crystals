using Fusion;
using UnityEngine;
using System;

public class LeverStateNetwork : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnIsLeftChanged))] private bool IsLeft { get; set; }
    public Action<bool> LeverInteractedAction;


    private bool _isSpawned;

    public override void Spawned()
    {
        _isSpawned = true;
        LeverInteractedAction?.Invoke(IsLeft);
    }

    public void RequestInteract()
    {
        if (!_isSpawned || Object == null)
        {
            Debug.LogWarning("LeverAnimator is not spawned yet.");
            return;
        }

        RPC_Interact();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private void RPC_Interact()
    {
        IsLeft = !IsLeft;
    }

    private void OnIsLeftChanged()
    {
        LeverInteractedAction?.Invoke(IsLeft);
    }

}