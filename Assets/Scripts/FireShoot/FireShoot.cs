using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class FireShoot : NetworkBehaviour
{
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float lifeTime = 6f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject visualPrefab;

    [Networked] private NetworkArray<ProjectileData> _projectilesBuffer { get; }

    [Networked] private int _totalFiredCount {get; set;}

    private int _visualsCreated;

    private List<VisualProjectileData> _currentActiveVisuals = new List<VisualProjectileData>();

    private struct ProjectileData : INetworkStruct
    {
        public int FireTick;
        public Vector3Compressed FirePosition;
        public Vector3Compressed FireVelocity;
        public NetworkBool IsAlive;
    }

    private class VisualProjectileData
    {
        public GameObject gameObject;
        public int BufferIndex;
    }

    public void Fire(Vector3 firePosition, Vector3 fireDirection)
    {
        var entry = _totalFiredCount % _projectilesBuffer.Length;

        var data = new ProjectileData()
        {
            FireTick = Runner.Tick,
            FirePosition = firePosition,
            FireVelocity = fireDirection * projectileSpeed,
            IsAlive = true
        };

        _projectilesBuffer.Set(entry, data);

        _totalFiredCount++;
    }

    private Vector3 GetPosition(ProjectileData data, int tick)
    {
        var elapsed = (tick - data.FireTick) * Runner.DeltaTime;

        if (elapsed <= 0f)
        {
            return data.FirePosition;
        }

        return data.FirePosition +
            (Vector3)data.FireVelocity * elapsed;
    }

    public override void FixedUpdateNetwork()
    {
        for (int i = 0; i < _totalFiredCount; i++)
        {
            var entryIndex = i % _projectilesBuffer.Length;
            var bufferData = _projectilesBuffer[entryIndex];

            if (!bufferData.IsAlive) continue;

            var currentPos = GetPosition(bufferData, Runner.Tick);
            var nextPos = GetPosition(bufferData, Runner.Tick + 1);
            var moveDirection = nextPos - currentPos;

            /* for later
            if (Runner.GetPhysicsScene().Raycast(
                    currentPos,
                    moveDirection.normalized,
                    out var hit,
                    moveDirection.magnitude,
                    targetLayer))
            {
                var player =
                    hit.collider.GetComponentInParent<Player>();

                if (player != null &&
                    player.Object.InputAuthority != Object.InputAuthority)
                {
                    player.Health.TakeHit(
                        damage: 1,
                        reportKill: true);
                }

                bufferData.IsAlive = false;
                _projectilesBuffer.Set(entryIndex, bufferData);
            }
            else if ((Runner.Tick - bufferData.FireTick)
                    * Runner.DeltaTime > lifeTime)
            {
                bufferData.IsAlive = false;
                _projectilesBuffer.Set(entryIndex, bufferData);
            } */
        }
    }

    public override void Render()
    {
        var renderTime = HasStateAuthority
            ? Runner.LocalRenderTime
            : Runner.RemoteRenderTime;

        while (_visualsCreated < _totalFiredCount)
        {
            var entry = _visualsCreated % _projectilesBuffer.Length;

            CleanVisualsAtEntryIndex(entry);

            VisualProjectileData data = new VisualProjectileData()
            {
                gameObject = Instantiate(visualPrefab),
                BufferIndex = entry,
            };

            _currentActiveVisuals.Add(data);
            _visualsCreated++;
        }

        for (int i = _currentActiveVisuals.Count - 1; i >= 0; i--)
        {
            var visual = _currentActiveVisuals[i];
            var bufferData = _projectilesBuffer[visual.BufferIndex];

            var elapsed = renderTime - bufferData.FireTick * Runner.DeltaTime;

            if (!bufferData.IsAlive)
            {
                Destroy(visual.gameObject);
                _currentActiveVisuals.RemoveAt(i);
                continue;
            }

            if (elapsed <= 0f) continue;

            var position = bufferData.FirePosition +
                        (Vector3)bufferData.FireVelocity * elapsed;

            visual.gameObject.transform.position = position;
            visual.gameObject.transform.rotation =
                Quaternion.LookRotation((Vector3)bufferData.FireVelocity);
        }
    }

    private void CleanVisualsAtEntryIndex(int index)
    {
        for (int i = _currentActiveVisuals.Count - 1; i >= 0; i--)
        {
            if (_currentActiveVisuals[i].BufferIndex == index)
            {
                if (_currentActiveVisuals[i].gameObject != null)
                    Destroy(_currentActiveVisuals[i].gameObject);

                _currentActiveVisuals.RemoveAt(i);
            }
        }
    }
}