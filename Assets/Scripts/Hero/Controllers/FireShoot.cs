using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class FireShoot : NetworkBehaviour
{
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float lifeTime = 6f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private ObjectPool _bulletPool;

    [Networked, Capacity(10)] private NetworkArray<ProjectileData> _projectilesBuffer { get; }
    [Networked] private int _totalFiredCount { get; set; }

    private int _visualsCreated;
    private readonly List<VisualProjectileData> _currentActiveVisuals = new();

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

    private void Awake()
    {
        if (_bulletPool == null)
        {
            _bulletPool = FindFirstObjectByType<ObjectPool>();
        }
    }

    public override void Spawned()
    {
        if (_bulletPool == null)
        {
            _bulletPool = FindFirstObjectByType<ObjectPool>();
        }
    }

    public void SetBulletPool(ObjectPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void Fire(Vector3 firePosition, Vector3 fireDirection)
    {
        if (!Object.HasInputAuthority)
            return;

        var entry = _totalFiredCount % _projectilesBuffer.Length;

        var data = new ProjectileData()
        {
            FireTick = Runner.Tick,
            FirePosition = firePosition,
            FireVelocity = fireDirection.normalized * projectileSpeed,
            IsAlive = true
        };

        _projectilesBuffer.Set(entry, data);
        _totalFiredCount++;
    }

    private Vector3 GetPosition(ProjectileData data, int tick)
    {
        var elapsed = (tick - data.FireTick) * Runner.DeltaTime;

        if (elapsed <= 0f)
            return data.FirePosition;

        return data.FirePosition + (Vector3)data.FireVelocity * elapsed;
    }

    public override void FixedUpdateNetwork()
    {
        for (int i = 0; i < _totalFiredCount; i++)
        {
            var entryIndex = i % _projectilesBuffer.Length;
            var bufferData = _projectilesBuffer[entryIndex];

            if (!bufferData.IsAlive)
                continue;

            var currentPos = GetPosition(bufferData, Runner.Tick);
            var nextPos = GetPosition(bufferData, Runner.Tick + 1);
            var moveDirection = nextPos - currentPos;

            if (moveDirection.sqrMagnitude <= 0.000001f)
                continue;

            if (Runner.GetPhysicsScene().Raycast(
                    currentPos,
                    moveDirection.normalized,
                    out var hit,
                    moveDirection.magnitude,
                    targetLayer))
            {

                var damageableObject = hit.collider.GetComponentInParent<IDamageable>();
                if (damageableObject != null)
                {
                    damageableObject.TakeDamage(1);
                }

                bufferData.IsAlive = false;
                _projectilesBuffer.Set(entryIndex, bufferData);
            }
            else if ((Runner.Tick - bufferData.FireTick) * Runner.DeltaTime > lifeTime)
            {
                bufferData.IsAlive = false;
                _projectilesBuffer.Set(entryIndex, bufferData);
            }
        }
    }

    public override void Render()
    {
        if (_bulletPool == null)
        {
            Debug.LogWarning("FireShoot.Render: _bulletPool is null");
            return;
        }

        var renderTime = Object.HasInputAuthority
            ? Runner.LocalRenderTime
            : Runner.RemoteRenderTime;

        while (_visualsCreated < _totalFiredCount)
        {
            var entry = _visualsCreated % _projectilesBuffer.Length;

            CleanVisualsAtEntryIndex(entry);

            VisualProjectileData data = new VisualProjectileData()
            {
                gameObject = _bulletPool.GetObject(),
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
                _bulletPool.ReturnObject(visual.gameObject);
                _currentActiveVisuals.RemoveAt(i);
                continue;
            }

            if (elapsed <= 0f)
                continue;

            var position = bufferData.FirePosition +
                           (Vector3)bufferData.FireVelocity * elapsed;

            visual.gameObject.transform.position = position;

            Vector3 velocity = (Vector3)bufferData.FireVelocity;
            if (velocity.sqrMagnitude > 0.0001f)
            {
                visual.gameObject.transform.rotation = Quaternion.LookRotation(velocity);
            }
        }
    }

    private void CleanVisualsAtEntryIndex(int index)
    {
        for (int i = _currentActiveVisuals.Count - 1; i >= 0; i--)
        {
            if (_currentActiveVisuals[i].BufferIndex == index)
            {
                if (_currentActiveVisuals[i].gameObject != null)
                    _bulletPool.ReturnObject(_currentActiveVisuals[i].gameObject);

                _currentActiveVisuals.RemoveAt(i);
            }
        }
    }
}