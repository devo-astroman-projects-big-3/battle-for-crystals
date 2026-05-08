using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef PlayerPrefab;
    public Action<GameObject> PlayerSpawwnedAction;

    [SerializeField] private Transform _playerParent;
    [SerializeField] private Transform[] _spawnPositions;

    private NetworkRunner _runner;
    private bool _localPlayerSpawned;

    

    private IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            _runner = FindFirstObjectByType<NetworkRunner>();
            return _runner != null && _runner.IsRunning;
        });

        _runner.AddCallbacks(this);
     

        foreach (PlayerRef player in _runner.ActivePlayers)
        {
            TrySpawnPlayer(player);
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        TrySpawnPlayer(player);
    }

    
    public void RestartGameplay()
    {
        _runner.LoadScene(SceneRef.FromIndex(1));
    }

    private void TrySpawnPlayer(PlayerRef player)
    {
        if (player != _runner.LocalPlayer)
            return;

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
        heroBuddy.SetId(player.PlayerId);
        heroBuddy.SetupParent(_playerParent);
        heroBuddy.ActiveCamera();
        heroBuddy.ActiveCanvas();
        heroBuddy.ActiveMainHero();
        heroBuddy.ActiveHeroFsm();
        heroBuddy.ActiveControllers();
        heroBuddy.ActiveHeroTagAndLayer();
        heroBuddy.SetupBornPosition(spawnPosition);

        Debug.Log($"player spawwer id {player.PlayerId}");
        PlayerSpawwnedAction?.Invoke(playerObject.gameObject);
        
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