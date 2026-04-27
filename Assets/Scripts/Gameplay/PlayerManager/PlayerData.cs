using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Id;
    public string Name;
    public GameObject PlayerGo;

    public PlayerData(int id, string name, GameObject playerGo)
    {
        Id = id;
        Name = name;
        PlayerGo = playerGo;
    }
}