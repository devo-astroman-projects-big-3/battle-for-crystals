using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    //public GameObject PlayerPrefab;
    [SerializeField] private NetworkPrefabRef PlayerPrefab;
    public Action<GameObject> PlayerSpawwnedAction;

    [SerializeField] private Transform _playerParent;
    [SerializeField] private Transform[] _spawnPositions;

    private NetworkRunner _runner;
    private bool _localPlayerSpawned;
    private bool _remotePlayerSpawned;

    

    private IEnumerator Start()
    {
        Debug.Log("Waiting for running NetworkRunner...");

        yield return new WaitUntil(() =>
        {
            _runner = FindFirstObjectByType<NetworkRunner>();
            return _runner != null && _runner.IsRunning;
        });

        _runner.AddCallbacks(this);

        Debug.Log("PlayerSpawner ready. Checking existing players...");

        foreach (PlayerRef player in _runner.ActivePlayers)
        {
            Debug.Log("player => " + player.PlayerId);

            TrySpawnPlayer(player);
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"FLLLAG FOR SECON PLAYER JOINNING Gameplay PlayerJoined: {player}");
        TrySpawnPlayer(player);
    }

    private void TrySpawnPlayer(PlayerRef player)
    {
        TrySpawnPlayer2(player);
        
        /* if (_localPlayerSpawned)
            return;

        if (player != _runner.LocalPlayer)
            return;

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
        heroBuddy.ActiveCamera();

        PlayerSpawwnedAction?.Invoke(playerObject.gameObject);

        Debug.Log("Local player spawned."); */
        
    }

    private void TrySpawnPlayer2(PlayerRef player)
    {
        
        if (player == _runner.LocalPlayer)
        {
            Debug.Log("LOCAL PLAYER " + player.PlayerId);

            if (_localPlayerSpawned)
                return;

            _localPlayerSpawned = true;

            Vector3 spawnPosition = GetSpawnPosition();

            NetworkObject playerObject = _runner.Spawn(
                PlayerPrefab,
                spawnPosition,
                Quaternion.identity,
                player
            );
            

            HeroBuddy heroBuddy = playerObject.GetComponent<HeroBuddy>();
            heroBuddy.SetupParent(_playerParent);
            heroBuddy.ActiveCamera();
            heroBuddy.SetupBornPosition(spawnPosition);

            PlayerSpawwnedAction?.Invoke(playerObject.gameObject);

            Debug.Log("Local player spawned.");
        }
        /* else
        {
            Debug.Log("REMOTE PLAYER " + player.PlayerId);
            if (_remotePlayerSpawned)
                return;
            _remotePlayerSpawned = true;


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

            Debug.Log("Remote player spawned.");
            
        } */

        
        
        
    }

    private Vector3 GetSpawnPosition()
    {
        if (_spawnPositions == null || _spawnPositions.Length == 0)
            return Vector3.zero;

        int playerCount = 0;

        foreach (PlayerRef p in _runner.ActivePlayers)
            playerCount++;

        int index = Mathf.Clamp(playerCount - 1, 0, _spawnPositions.Length - 1);

        return _spawnPositions[index].position;
    }

    private void OnDestroy()
    {
        if (_runner != null)
            _runner.RemoveCallbacks(this);
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {}
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {}
    public void OnConnectedToServer(NetworkRunner runner) {}
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList){}
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) {}
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {}
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {}
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {}
    public void OnInput(NetworkRunner runner, NetworkInput input) {}
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {}
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) {}
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) {}
    public void OnSceneLoadDone(NetworkRunner runner) {}
    public void OnSceneLoadStart(NetworkRunner runner) {}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {}
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}
}