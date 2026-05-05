using Fusion;
using UnityEngine;
using System;
using System.Collections;

public class PlayerSpawner : MonoBehaviour, IPlayerJoined
//public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Action<GameObject> PlayerSpawwnedAction;

    [SerializeField] private Transform _playerParent;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private NetworkRunner _runner;

    private bool _localPlayerSpawned;

    private IEnumerator Start()
    {
        Debug.Log("Waiting for running NetworkRunner...");

        yield return new WaitUntil(() =>
        {
            _runner = FindFirstObjectByType<NetworkRunner>();
            return _runner != null && _runner.IsRunning;
        });

        Debug.Log("Waiting for active players...");

        yield return new WaitUntil(() =>
        {
            foreach (PlayerRef player in _runner.ActivePlayers)
                return true;

            return false;
        });

        Debug.Log("PlayerSpawner ready. Checking existing players...");

        foreach (PlayerRef player in _runner.ActivePlayers)
        {
            TrySpawnPlayer(player);
        }
    }

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        Debug.Log("Gameplay PlayerJoined !!!");
        TrySpawnPlayer(player);
    }

    private void TrySpawnPlayer(PlayerRef player)
    {
        Debug.Log("Flag0");
        if (_localPlayerSpawned)
            return;

        Debug.Log("Flag1");
        if (player != _runner.LocalPlayer)
            return;

        Debug.Log("Flag2"); 
        _localPlayerSpawned = true;

        Vector3 spawnPosition = GetSpawnPosition();

        NetworkObject playerObject = _runner.Spawn(
            PlayerPrefab,
            spawnPosition,
            Quaternion.identity,
            player
        );

        HeroBuddy heroBuddy = playerObject.GetComponent<HeroBuddy>();

        heroBuddy.SetupBornPosition(spawnPosition);
        heroBuddy.SetupParent(_playerParent);

        PlayerSpawwnedAction?.Invoke(playerObject.gameObject);

        Debug.Log("Local player spawned.");
    }

    private Vector3 GetSpawnPosition()
    {
        int playerCount = 0;

        foreach (PlayerRef p in _runner.ActivePlayers)
            playerCount++;

        int index = playerCount - 1;
        index = Mathf.Clamp(index, 0, _spawnPositions.Length - 1);

        return _spawnPositions[index].position;
    }
}