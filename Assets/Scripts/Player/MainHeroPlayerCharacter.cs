using UnityEngine;

public class MainHeroPlayerCharacter : MonoBehaviour
{
    #region Fields
    [SerializeField] ThirdPersonAim _thirdPersonAim;
    [SerializeField] FireShoot _fireShoot;
    [SerializeField] HeroHealth _heroHealth;
    [SerializeField] HeroHitGlow _heroHitGlow;
    [SerializeField] HeroDead _heroDead;
    
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
      if(_heroHealth.Life > 0)
      {
         _heroHealth.DecreaseLife(1);

         _heroHitGlow.MakeGlowHitOn();
         if(_heroHealth.Life == 0)
         {
            Debug.Log(">>>>>>>Hero Dead");
            gameObject.layer = LayerMask.NameToLayer("DeadPlayer");
            gameObject.tag = "DeadPlayer";
            _heroDead.DeadDelay(2);
         }
         else
         {
            Debug.Log(">>>>>>>Hero Health " + _heroHealth.Life);
            _heroHealth.IgnoreHurtForSecs(1);
         }
      }

       
    }
    #endregion

    #region Private Methods
    #endregion
}
