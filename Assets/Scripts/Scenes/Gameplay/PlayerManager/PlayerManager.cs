using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    #region Private Fields
    private List<PlayerData> _allPlayersData = new List<PlayerData>();
    private GameObject _playerInputAthority;
    #endregion

    #region Public Methods

    public void SetPlayerInputAuthority(GameObject player)
    {
        _playerInputAthority = player;
    } 

    public void AddPlayer(GameObject newPlayerGo)
    {
        Debug.Log("PlayerManager - player: " + newPlayerGo.name);


        int id = _allPlayersData.Count + 1;
        string playerName = "player" + id;

        PlayerData playerData = new PlayerData(id, playerName, newPlayerGo);

        _allPlayersData.Add(playerData);

        newPlayerGo.name = playerName;

        Debug.Log("Added: " + playerName);
    }
    
    public void UpdateCrystalData(int[] crystalData)
    {   
        MainHero mainHero = _playerInputAthority.GetComponentInChildren<MainHero>(true);
        mainHero.SetCrystalsData(crystalData);
    }

    public void DeactivateControlInputs()
    {   
        MainHero mainHero = _playerInputAthority.GetComponentInChildren<MainHero>(true);

        mainHero.BlockInputs();
    }

    public int GetNCrystalsCollected()
    {
        MainHero mainHero = _playerInputAthority.GetComponentInChildren<MainHero>(true);

        return mainHero.GetNCrystalsCollected();
    }

    public void NotifyPlayerLeft()
    {
        MainHero mainHero = _playerInputAthority.GetComponentInChildren<MainHero>(true);

        mainHero.SetNetMsg("other player has left");
    }

    public void NotifyPlayerJoined()
    {
        MainHero mainHero = _playerInputAthority.GetComponentInChildren<MainHero>(true);

        mainHero.SetNetMsg("other arrived");
    }

    public void NotifyRedirectingToLobby()
    {
        MainHero mainHero = _playerInputAthority.GetComponentInChildren<MainHero>(true);

        mainHero.SetNetMsg("Admin left - so redirecting to Lobby");

    }
    #endregion
}
