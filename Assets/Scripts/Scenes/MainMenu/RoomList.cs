using System;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    [SerializeField] GameObject parentList;
    [SerializeField] GameObject entryPrefab;

    public Action<string> RoomSelectedAction;
    
    void Start()
    { 
    }

    public void SetRooms(SessionData[] roomsData)
    {

        Transform parentTransform = parentList.transform;

        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            GameObject entry = parentTransform.GetChild(i).gameObject;
            RoomEntryData roomEntryData = entry.GetComponent<RoomEntryData>();

            roomEntryData.JoinButtonClickAction -= HandleJoinButtonClick;

            Destroy(entry);
        }

        if (roomsData == null)
            return;

        foreach (SessionData roomData in roomsData)
        {
            GameObject entry = Instantiate(entryPrefab, parentTransform);

            RoomEntryData roomEntryData = entry.GetComponent<RoomEntryData>();

            roomEntryData.JoinButtonClickAction += HandleJoinButtonClick;

            roomEntryData.SetData(roomData.Name,roomData.PlayerCount,roomData.MaxPlayers,roomData.IsOpen);
        }

    }

    void OnDestroy()
    {
     
    }

    private void HandleJoinButtonClick(string roomName)
    {
        RoomSelectedAction?.Invoke(roomName);
    }

}
