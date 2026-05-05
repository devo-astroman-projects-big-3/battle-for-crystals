using System;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SharedLobby _sharedLobby;
    [SerializeField] private GameObject _roomCreationPanelGO;
    [SerializeField] private GameObject _joinRoomPanelGO;
    [SerializeField] private TMP_InputField _nameRoomInput;

    void Awake()
    {
        if (_nameRoomInput == null)
            _nameRoomInput = GetComponentInChildren<TMP_InputField>(true);
    }
    
    void Start()
    {
        SetRoomCreationPanelActive(true);
    }

    public void OnRoomCreationPanelButtonClicked()
    {
        Debug.Log("OnRoomCreationPanelButtonClicked");
        SetRoomCreationPanelActive(true);

        //_sharedLobby.CreateSharedRoom("room1");
    }

    public void OnJoinRoomPanelButtonClicked()
    {
        Debug.Log("OnJoinRoomPanelButtonClicked");
        SetRoomCreationPanelActive(false);

        //_sharedLobby.CreateSharedRoom("room1");
    }

    public void OnCreateRoomButtonClicked()
    {
        Debug.Log("OnCreateRoomButtonClicked");
        string roomName = GetRoomName();

        _sharedLobby.CreateSharedRoom(roomName);
    }

/*     public void OnJoinRoomButtonClicked()
    {
        Debug.Log("OnJoinRoomButtonClicked");
        string roomName = GetRoomName();

        _sharedLobby.JoinSharedRoom(roomName);
    } */

    private string GetRoomName()
    {
        string roomName = _nameRoomInput.text.Trim();
        if (string.IsNullOrEmpty(roomName)) {            
            return "room" + DateTime.Now;
        }

        return roomName;
    }

    private void SetRoomCreationPanelActive(bool isActive)
    {
        if (_roomCreationPanelGO != null)
            _roomCreationPanelGO.SetActive(isActive);
        else
            Debug.LogError($"{nameof(MainMenu)} is missing a room creation panel reference.", this);

        if (_joinRoomPanelGO != null)
            _joinRoomPanelGO.SetActive(!isActive);
        else
            Debug.LogError($"{nameof(MainMenu)} is missing a join room panel reference.", this);
    }

}
