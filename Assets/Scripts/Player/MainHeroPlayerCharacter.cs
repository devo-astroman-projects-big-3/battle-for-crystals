using UnityEngine;

public class MainHeroPlayerCharacter : MonoBehaviour
{
    #region Fields
    [SerializeField] ThirdPersonAim _thirdPersonAim;
    [SerializeField] FireShoot _fireShoot;
    [SerializeField] HeroHealth _heroHealth;

    
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

   public void ReceiveDamage(int damageValue)
    {
       _heroHealth.DecreaseLife(1);

      if(_heroHealth.Life == 0)
      {
         Debug.Log("Hero Dead");
      }
    }
    #endregion

    #region Private Methods
    #endregion
}