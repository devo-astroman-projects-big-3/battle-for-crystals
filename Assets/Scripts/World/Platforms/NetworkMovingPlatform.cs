using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(NetworkTransform))]
public class NetworkMovingPlatform : NetworkBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [Header("Movement")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _waitTimeAtPoints = 0.5f;

    [Networked] private int TargetPointIndex { get; set; } // 0 = A, 1 = B
    [Networked] private TickTimer WaitTimer { get; set; }

    private Vector3 CurrentTargetPosition =>
        TargetPointIndex == 0 ? _pointA.position : _pointB.position;

    public override void Spawned()
    {
        if (_pointA == null || _pointB == null)
        {
            Debug.LogError("NetworkMovingPlatform: Point A or Point B is missing.");
            enabled = false;
            return;
        }

        if (HasStateAuthority)
        {
            transform.position = _pointA.position;
            TargetPointIndex = 1; // start moving toward B
            WaitTimer = TickTimer.None;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
            return;

        if (_pointA == null || _pointB == null)
            return;

        if (WaitTimer.IsRunning)
        {
            if (WaitTimer.Expired(Runner))
            {
                WaitTimer = TickTimer.None;
            }
            else
            {
                return;
            }
        }

        Vector3 targetPosition = CurrentTargetPosition;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            _speed * Runner.DeltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            TargetPointIndex = TargetPointIndex == 0 ? 1 : 0;

            if (_waitTimeAtPoints > 0f)
            {
                WaitTimer = TickTimer.CreateFromSeconds(Runner, _waitTimeAtPoints);
            }
        }
    }
}