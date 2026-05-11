using Fusion;
using UnityEngine;
using System.Collections;

public class MainGameplay : MonoBehaviour
{
    #region Fields
    [SerializeField] PlayerSpawner _playerSpawner;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] CrystalManager _crystalManager;
    [SerializeField] GameSceneManager _gameSceneManager;
    [SerializeField] NetManager _netManager;
    private NetworkRunner _runner;
    #endregion
    
    #region Private Fields
    private int nCrystals = 0;
    private Coroutine _goResultsRoutine;
    private Coroutine _goLobbyRoutine;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        AllGameData.nCrystalTaken = 0;
        _playerSpawner.PlayerSpawwnedAction += HandlePlayerSpawwned;
        int[] crystalsData = _crystalManager.GetCrystalStates();

        _crystalManager.CrystalsChangedAction += HandleCrystalsChanged;

        _netManager.PlayerLeftAction += HandlePlayerLeft;
        _netManager.PlayerJoinedAction += HandlePlayerJoined;

    }

    void OnDestroy()
    {
        _playerSpawner.PlayerSpawwnedAction -= HandlePlayerSpawwned;
        _crystalManager.CrystalsChangedAction -= HandleCrystalsChanged;
        _netManager.PlayerLeftAction -= HandlePlayerLeft;
        _netManager.PlayerJoinedAction -= HandlePlayerJoined;
    }
    #endregion
    #region Public Methods
    #endregion

    #region Private Methods
    private void HandlePlayerSpawwned(GameObject playerConnected)
    {
        _playerManager.SetPlayerInputAuthority(playerConnected);
        _runner = _playerSpawner.GetNetworkRunner();
        _netManager.SetNetworkRunner(_runner);
    }

    private void HandleCrystalsChanged(bool allCrystalsTaken)
    {
        int[] crystalsData = _crystalManager.GetCrystalStates();
        _playerManager.UpdateCrystalData(crystalsData);
        nCrystals++;

        if(nCrystals == 2)
        //if (allCrystalsTaken)
        {
            ProcessEndBattle();
        }

    }

    private void ProcessEndBattle()
    {
        _playerManager.DeactivateControlInputs();

        AllGameData.nCrystalTaken = _playerManager.GetNCrystalsCollected();

        _goResultsRoutine = StartCoroutine(GoShowResultsRoutine(2));
    }

    private IEnumerator GoShowResultsRoutine(int secs)
    {
        yield return new WaitForSeconds(secs);

        _gameSceneManager.GoResults();
        _goResultsRoutine = null;
    }

    private void HandlePlayerLeft()
    {
        if (AllGameData.isAdmin)
        {
            _playerManager.NotifyPlayerLeft();
        }
        else
        {
            _playerManager.NotifyRedirectingToLobby();
            _goLobbyRoutine = StartCoroutine(GoLobbyRoutine(4));
        }
    }

    private void HandlePlayerJoined()
    {
        _playerManager.NotifyPlayerJoined();
    }

    private IEnumerator GoLobbyRoutine(int secs)
    {
        yield return new WaitForSeconds(secs);
        _gameSceneManager.GoLobby();
        _goLobbyRoutine = null;
    }
    
    #endregion


}