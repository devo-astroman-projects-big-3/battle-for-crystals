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
        /* Debug.Log("--> UpdateCrystalData " + crystalData);

        for (int i = 0; i < crystalData.Length; i++)
        {
            Debug.Log($"index: {i} _  data: {crystalData[i]} ");
        } */

        
        MainHero mainHero = _playerInputAthority.GetComponentInChildren<MainHero>(true);
        mainHero.SetCrystalsData(crystalData);
    }
    #endregion
}
