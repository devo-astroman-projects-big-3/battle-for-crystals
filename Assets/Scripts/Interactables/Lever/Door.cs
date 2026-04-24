using UnityEngine;

public class Door : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject _door;
    #endregion

    #region Private Fields    
    #endregion

    #region Unity Callbacks    
    #endregion

    #region Public Methods
    public void OpenDoor()
    {
        _door.SetActive(false);
    }

    public void CloseDoor()
    {
        _door.SetActive(true);
    }
    #endregion

    #region Private Methods    
    #endregion
}