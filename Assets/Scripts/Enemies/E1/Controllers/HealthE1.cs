using Fusion;
using UnityEngine;

public class HealthE1: NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(HealthChanged))]
    public int NetworkedHealth { get; set; } = 5;

    private void HealthChanged()
    {
        Debug.Log($"Health changed to: {NetworkedHealth}");
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        NetworkedHealth -= damage;
    }
}