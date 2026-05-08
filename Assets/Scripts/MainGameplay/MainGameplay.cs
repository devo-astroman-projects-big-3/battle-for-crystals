using UnityEngine;
using System;

public class MainGameplay : MonoBehaviour
{
    #region Fields
    [SerializeField] PlayerSpawner _playerSpawner;
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] CrystalManager _crystalManager;

    #endregion
    #region Public Fields    
    #endregion

    #region Unity Callbacks
    void Start()
    {
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

    private void HandleCrystalsChanged()
    {
        int[] crystalsData = _crystalManager.GetCrystalStates();
        _playerManager.UpdateCrystalData(crystalsData);
    }
    
    #endregion


}