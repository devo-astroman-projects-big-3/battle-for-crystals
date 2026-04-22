using UnityEngine;

public class E1 : MonoBehaviour
{
    #region Fields
    [SerializeField] AnimatorE1 _animatorE1;
    #endregion

    #region Unity Callbacks
    #endregion
    #region Public Methods
    public void HideEnemy()
    {
        _animatorE1.PlayIdleHideDown();
    }

    public void ShowEnemy()
    {
        _animatorE1.PlayIdleShowUp();
    }

    public void Fire()
    {
        _animatorE1.PlayFire();
    }

    public void ReceiveDamage()
    {
        _animatorE1.PlayReceiveDamage();
    }
    #endregion
}