using UnityEngine;


public class MainHero : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject virtualCamera;
    #endregion

    #region Private Fields
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void ActiveCamera()
    {
        mainCamera.SetActive(true);
        virtualCamera.SetActive(true);
    }

    #endregion
}
