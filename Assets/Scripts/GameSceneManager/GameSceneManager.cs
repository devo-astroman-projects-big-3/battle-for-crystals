using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour, INetworkRunnerCallbacks
{
    
    [Header("Scene Names (must match Build Settings)")]
    [SerializeField] private int sceneLobbyIndex = 0;
    [SerializeField] private int gameplayIndex = 1;
    [SerializeField] private int resultIndex = 2;

    private NetworkRunner _runner;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            _runner = FindFirstObjectByType<NetworkRunner>();
            return _runner != null && _runner.IsRunning;
        });

        _runner.AddCallbacks(this);
    }
    
    public void GoLobby()
    {
        //LoadScene(sceneLobbyIndex);
        ReturnToLobby();
    }

    public void GoGameplay()
    {
        LoadScene(gameplayIndex);
    }

    public void GoResults()
    {
        LoadScene(resultIndex);
    }


    private void LoadScene(int sceneIndex)
    {
       _runner.LoadScene(SceneRef.FromIndex(sceneIndex));
    }

    public async void ReturnToLobby()
    {
        await _runner.Shutdown();

        SceneManager.LoadScene(sceneLobbyIndex);
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
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player){}
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {}
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) {}
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) {}
    public void OnSceneLoadDone(NetworkRunner runner) {}
    public void OnSceneLoadStart(NetworkRunner runner) {}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {}
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}

}
