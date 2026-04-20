using UnityEngine;

public class MainHeroPlayerCharacter : MonoBehaviour
{
    #region Fields
    [SerializeField] ThirdPersonAim _thirdPersonAim;
    #endregion

    #region Private Fields
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void SetCamera(Camera camera)
    {
       _thirdPersonAim.SetCamera(camera);
    }
    #endregion

    #region Private Methods
    #endregion
}