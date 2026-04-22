using UnityEngine;

public class AnimatorE1 : MonoBehaviour
{
    #region Fields
    [SerializeField] Animator _animator;
    #endregion

    #region Unity Callbacks
    #endregion
    #region Public Methods
    public void PlayIdleHideDown()
    {
        _animator.SetTrigger("HideDown");
    }

    public void PlayIdleShowUp()
    {
        _animator.SetTrigger("ShowUp");
    }

    public void PlayFire()
    {
        _animator.SetTrigger("Fire");
    }
    public void PlayReceiveDamage()
    {
        _animator.SetTrigger("ReceiveDamage");
    }
    #endregion
}