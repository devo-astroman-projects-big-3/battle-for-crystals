using System;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SharedLobby _sharedLobby;
    [SerializeField] private GameObject _roomCreationPanelGO;
    [SerializeField] private GameObject _joinRoomPanelGO;
    [SerializeField] private TMP_InputField _nameRoomInput;
    [SerializeField] private RoomList _roomList;

    [SerializeField] private GameObject _openJoinPanelButtonGO;
    [SerializeField] private GameObject _openCreationPanelButtonGO;

    void Awake()
    {
        if (_nameRoomInput == null)
            _nameRoomInput = GetComponentInChildren<TMP_InputField>(true);
    }
    
    void Start()
    {
        SessionData[] sessionData = _sharedLobby.GetSessionData();
        _roomList.SetRooms(sessionData);

        _roomList.RoomSelectedAction +=  HandleRoomSelected;
        _sharedLobby.SessionDataListChangeAction += HandleSessionDataListChange;
        SetRoomCreationPanelActive(true);
    }

    void OnDestroy()
    {
        _roomList.RoomSelectedAction -=  HandleRoomSelected;
        _sharedLobby.SessionDataListChangeAction -= HandleSessionDataListChange;
    }

    public void OnRoomCreationPanelButtonClicked()
    {
        SetRoomCreationPanelActive(true);
        _openJoinPanelButtonGO.SetActive(true);
        _openCreationPanelButtonGO.SetActive(false);
    }

    public void OnJoinRoomPanelButtonClicked()
    {
        SetRoomCreationPanelActive(false);
        _openJoinPanelButtonGO.SetActive(false);
        _openCreationPanelButtonGO.SetActive(true);
    }

    public void OnCreateRoomButtonClicked()
    {
        string roomName = GetRoomName();

        _sharedLobby.CreateSharedRoom(roomName);
    }

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

    private void HandleSessionDataListChange(SessionData[] sessionData)
    {
        _roomList.SetRooms(sessionData);
    }

    private void HandleRoomSelected(string roomName)
    {
        _sharedLobby.JoinSharedRoom(roomName);
    }

}
