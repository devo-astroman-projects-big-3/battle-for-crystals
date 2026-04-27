using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    #region Private Fields
    private List<PlayerData> _allPlayersData = new List<PlayerData>();
    #endregion

    #region Public Methods
    public void AddPlayer(GameObject newPlayerGo)
    {
        int id = _allPlayersData.Count + 1;
        string playerName = "player" + id;

        PlayerData playerData = new PlayerData(id, playerName, newPlayerGo);

        _allPlayersData.Add(playerData);

        newPlayerGo.name = playerName;

        Debug.Log("Added: " + playerName);
    }

    #endregion
}