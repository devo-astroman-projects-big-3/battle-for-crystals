using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawnerTest : MonoBehaviour, INetworkRunnerCallbacks
{
    //public GameObject PlayerPrefab;
    [SerializeField] private NetworkPrefabRef PlayerPrefab;    

    private NetworkRunner _runner;
    private bool _localPlayerSpawned;
    

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
        Debug.Log($"Test FLLLAG FOR SECON PLAYER JOINNING Gameplay PlayerJoined: {player}");
        TrySpawnPlayer(player);
    }

    private void TrySpawnPlayer(PlayerRef player)
    {
        TrySpawnPlayer2(player);
    }

    private void TrySpawnPlayer2(PlayerRef player)
    {
        
        if (_localPlayerSpawned)
                return;

            _localPlayerSpawned = true;

            NetworkObject playerObject = _runner.Spawn(
                PlayerPrefab,
                new Vector3(0,3,0),
                Quaternion.identity,
                player
            );
        
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