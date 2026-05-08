using UnityEngine;
using System;

public class MainGameplay : MonoBehaviour
{
    #region Fields
    [SerializeField] PlayerSpawner _playerSpawner;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] CrystalManager _crystalManager;
    [SerializeField] GameSceneManager _gameSceneManager;

    #endregion
    #region Private Fields
    private int nCrystals = 0;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        AllGameData.nCrystalTaken = 0;
        _playerSpawner.PlayerSpawwnedAction += HandlePlayerSpawwned;
        int[] crystalsData = _crystalManager.GetCrystalStates();

        _crystalManager.CrystalsChangedAction += HandleCrystalsChanged;
    }

    void OnDestroy()
    {
        _playerSpawner.PlayerSpawwnedAction -= HandlePlayerSpawwned;
        _crystalManager.CrystalsChangedAction -= HandleCrystalsChanged;
    }
    #endregion
    #region Public Methods
    #endregion

    #region Private Methods
    private void HandlePlayerSpawwned(GameObject playerConnected)
    {
        _playerManager.SetPlayerInputAuthority(playerConnected);
    }

    private void HandleCrystalsChanged(bool allCrystalsTaken)
    {
        int[] crystalsData = _crystalManager.GetCrystalStates();
        _playerManager.UpdateCrystalData(crystalsData);
        nCrystals++;

        if(nCrystals == 2)
        {
            Debug.Log("TIME TO RESTART");
            AllGameData.nCrystalTaken = _playerManager.GetNCrystalsCollected();

            _gameSceneManager.GoResults();
        }
        /* if (allCrystalsTaken)
        {
            _playerManager.DeactivateControlInputs();
            //_playerManager
            

            //_resultsManager.ShowResults();

            //deactivate inputs
            //show who is the winner
            //show time
            //go back to lobby
        } */
        
    }
    
    #endregion


}