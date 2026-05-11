using Fusion;
using UnityEngine;

public class HeroTeleporter : NetworkBehaviour
{
    [SerializeField] private CharacterController _controller;

    private bool _pendingTeleport;
    private Vector3 _targetPosition;

    public void Teleport(Vector3 position)
    {
        if (!Object.HasInputAuthority && !Object.HasStateAuthority)
            return;

        _targetPosition = position;
        _pendingTeleport = true;
    }

    public override void FixedUpdateNetwork()
    {
        if (!_pendingTeleport)
            return;

        _pendingTeleport = false;

        if (_controller != null)
            _controller.enabled = false;
        
        _controller.transform.position = _targetPosition;

        if (_controller != null)
            _controller.enabled = true;
        
    }
}