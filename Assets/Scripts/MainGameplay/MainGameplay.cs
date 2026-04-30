using UnityEngine;
using System;

public class MainGameplay : MonoBehaviour
{
    #region Fields
    [SerializeField] PlayerSpawner _playerSpawner;
    [SerializeField] PlayerManager _playerManager;

    #endregion
    #region Public Fields    
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _playerSpawner.PlayerSpawwnedAction += HandlePlayerSpawwned;
    }

    void OnDestroy()
    {
        _playerSpawner.PlayerSpawwnedAction -= HandlePlayerSpawwned;
    }
    #endregion
    #region Public Methods
    #endregion

    #region Private Methods
    private void HandlePlayerSpawwned(GameObject playerConnected)
    {
        _playerManager.AddPlayer(playerConnected);
    }
    #endregion


}