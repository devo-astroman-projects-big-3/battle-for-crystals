using UnityEngine;
using System.Collections;

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
    private Coroutine _goResultsRoutine;
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
            ProcessEndBattle();
            /* Debug.Log("TIME TO RESTART");
            AllGameData.nCrystalTaken = _playerManager.GetNCrystalsCollected(); */

            //_gameSceneManager.GoResults();
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

    private void ProcessEndBattle()
    {
        Debug.Log("TIME TO RESTART is admin: " + AllGameData.isAdmin);
        _playerManager.DeactivateControlInputs();

        AllGameData.nCrystalTaken = _playerManager.GetNCrystalsCollected();
        Debug.Log("AllGameData.nCrystalTaken -> " + AllGameData.nCrystalTaken);

        //show a message of battle finished

        _goResultsRoutine = StartCoroutine(GoShowResultsRoutine(2));

    }

    private IEnumerator GoShowResultsRoutine(int secs)
    {
        yield return new WaitForSeconds(secs);

        _gameSceneManager.GoResults();
        _goResultsRoutine = null;
    }
    
    #endregion


}