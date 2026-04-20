using UnityEngine;

public class MainHeroPlayerCharacter : MonoBehaviour
{
    #region Fields
    [SerializeField] ThirdPersonAim _thirdPersonAim;
    [SerializeField] FireShoot _fireShoot;
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

    public void SetBulletPool(ObjectPool bulletPool)
    {
       _fireShoot.SetBulletPool(bulletPool);
    }

    
    #endregion

    #region Private Methods
    #endregion
}