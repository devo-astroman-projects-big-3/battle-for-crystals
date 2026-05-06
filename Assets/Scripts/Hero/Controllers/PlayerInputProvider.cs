using Fusion;
using Fusion.Sockets;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public struct NetworkInputData : INetworkInput
{
    public Vector2 Move;
    public NetworkBool Jump;
    public NetworkBool Fire;
    public Vector3 AimPoint;
}
public class PlayerInputProvider : SimulationBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private ThirdPersonAim _thirdPersonAim;

    private bool _jumpPressed;
    private bool _firePressed;
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

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            _jumpPressed = true;

        if (Input.GetButtonDown("Fire1"))
            _firePressed = true;
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData data = new NetworkInputData();

        data.Move = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        data.Move = Vector2.ClampMagnitude(data.Move, 1f);

        data.Jump = _jumpPressed;
        data.Fire = _firePressed;

        if (_thirdPersonAim != null)
            data.AimPoint = _thirdPersonAim.GetAimPoint();

        _jumpPressed = false;
        _firePressed = false;

        input.Set(data);
    }

    // Other INetworkRunnerCallbacks can be empty
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player){}
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {}
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) {}
    public void OnConnectedToServer(NetworkRunner runner) {}
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList){}
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) {}
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {}
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {}
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {}    
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {}
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) {}
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) {}
    public void OnSceneLoadDone(NetworkRunner runner) {}
    public void OnSceneLoadStart(NetworkRunner runner) {}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {}
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {}
}