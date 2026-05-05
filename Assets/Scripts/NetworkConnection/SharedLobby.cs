using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SharedLobby : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner runnerPrefab;
    [SerializeField] private int gameSceneIndex = 1;
    [SerializeField] private int maxPlayers = 4;

    private NetworkRunner _runner;
    public List<SessionInfo> CurrentSessions = new();

    private async void Start()
    {
        if (runnerPrefab == null)
        {
            Debug.LogError($"{nameof(SharedLobby)} is missing a NetworkRunner prefab reference.", this);
            return;
        }

        CreateRunner();

        var result = await _runner.JoinSessionLobby(SessionLobby.Shared);

        if (!result.Ok)
            Debug.LogError($"Failed to join shared lobby: {result.ShutdownReason}", this);
    }

    private void CreateRunner()
    {
        Debug.Log("CreateRunner");
        _runner = Instantiate(runnerPrefab);
        _runner.name = "Shared Network Runner";
        _runner.AddCallbacks(this);
        Debug.Log("After CreateRunner");
    }

    private async Task RestartRunner()
    {
        if (_runner != null)
        {
            _runner.RemoveCallbacks(this);
            await _runner.Shutdown();

            Destroy(_runner.gameObject);
            _runner = null;

            await Task.Yield();
        }

        CreateRunner();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        CurrentSessions = new List<SessionInfo>(sessionList);

        foreach (var s in sessionList)
            Debug.Log($"Room: {s.Name} {s.PlayerCount}/{s.MaxPlayers} Open:{s.IsOpen}");
    }

    public async void CreateSharedRoom(string roomName)
    {
        Debug.Log("CreateSharedRoom");
        if (!CanStartRoom(roomName))
            return;

        await RestartRunner();
        Debug.Log("CreateSharedRoom_1");

        var result = await _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Single,
            SessionName = roomName,
            PlayerCount = maxPlayers,
            Scene = SceneRef.FromIndex(gameSceneIndex),
            SceneManager = GetOrCreateSceneManager()
        });

        Debug.Log("CreateSharedRoom_2");

        if (!result.Ok)
            Debug.LogError($"Failed to create shared room '{roomName}': {result.ShutdownReason}", this);

        Debug.Log("CreateSharedRoom_3");
    }

    public async void JoinSharedRoom(string roomName)
    {
        if (!CanStartRoom(roomName))
            return;

        await RestartRunner();

        var result = await _runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = roomName,
            SceneManager = GetOrCreateSceneManager()
        });

        if (!result.Ok)
            Debug.LogError($"Failed to join shared room '{roomName}': {result.ShutdownReason}", this);
    }

    private bool CanStartRoom(string roomName)
    {
        if (_runner == null)
        {
            Debug.LogWarning("Network runner is not ready yet.", this);
            return false;
        }

        if (string.IsNullOrWhiteSpace(roomName))
        {
            Debug.LogWarning("Room name cannot be empty.", this);
            return false;
        }

        return true;
    }

    private NetworkSceneManagerDefault GetOrCreateSceneManager()
    {
        if (!_runner.TryGetComponent(out NetworkSceneManagerDefault sceneManager))
            sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();

        return sceneManager;
    }

    private void OnDestroy()
    {
        if (_runner != null)
            _runner.RemoveCallbacks(this);
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
}