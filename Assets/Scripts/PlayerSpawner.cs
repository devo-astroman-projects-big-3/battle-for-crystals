using Fusion;
using UnityEngine;
using System;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public Action<GameObject> PlayerSpawwnedAction;

    [SerializeField] private Transform _playerParent;
    [SerializeField] private ObjectPool _objectPool;

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject playerObject = Runner.Spawn(
                PlayerPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            );

            HeroBuddy heroBuddy = playerObject.GetComponent<HeroBuddy>();
            heroBuddy.SetupParent(_playerParent);
        }
    }
}