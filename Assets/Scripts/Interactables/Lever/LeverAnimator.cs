using Fusion;
using UnityEngine;

public class LeverAnimator : NetworkBehaviour
{
    [SerializeField] private Animator _animator;

    private static readonly int IsLeftHash = Animator.StringToHash("IsLeft");

    [Networked] private bool IsLeft { get; set; }

    private bool _isSpawned;

    public override void Spawned()
    {
        _isSpawned = true;
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

    public override void Render()
    {
        if (_animator == null)
            return;

        _animator.SetBool(IsLeftHash, IsLeft);
    }
}