using Fusion;
using UnityEngine;
using System;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public Action<GameObject> PlayerSpawwnedAction;

    [SerializeField] private Transform _playerParent;
    [SerializeField] private ObjectPool _objectPool;

    [SerializeField] private Transform[] _spawnPositions;
    

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        Debug.Log("Gameplay " + "PlayerJoined");
        if (player == Runner.LocalPlayer)
        {
            
            Vector3 spawnPosition = Vector3.zero;

            NetworkObject playerObject = Runner.Spawn(
                PlayerPrefab,
                spawnPosition,
                Quaternion.identity,
                player
            );
            
            HeroBuddy heroBuddy = playerObject.GetComponent<HeroBuddy>();
            
            Vector3 bornPosition = GetSpawnPosition();
            heroBuddy.SetupBornPosition(bornPosition);
            heroBuddy.SetupParent(_playerParent);
        }
    }
    private Vector3 GetSpawnPosition()
    {
        int playerCount = 0;

        foreach (PlayerRef p in Runner.ActivePlayers)
        {
            playerCount++;
        }

        int index = playerCount - 1;
        index = Mathf.Clamp(index, 0, _spawnPositions.Length - 1);

        return _spawnPositions[index].position;
    }
}