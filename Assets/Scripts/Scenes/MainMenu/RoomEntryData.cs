using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomEntryData : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomNameText;
    [SerializeField] private TMP_Text _roomPlayersText;
    [SerializeField] private Button _joinButton;

    private string _roomName = "";

    public Action<string> JoinButtonClickAction;
    
    
    void Start()
    {
        _joinButton.onClick.AddListener(HandleOnClick);
    }

    public void SetData(string roomName, int playerCount, int maxPlayers, bool isOpen )
    {
        _roomName = roomName;
        _roomNameText.text = roomName;

        _roomPlayersText.text = $"{playerCount}/{maxPlayers}";

        _joinButton.interactable = isOpen && playerCount < maxPlayers;
    }

    


    void OnDestroy()
    {

        _joinButton.onClick.RemoveListener(HandleOnClick);
    }

    private void HandleOnClick()
    {
        JoinButtonClickAction?.Invoke(_roomName);
    }

}
