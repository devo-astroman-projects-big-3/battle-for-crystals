using System;
using UnityEngine;

public class AnimatorE1 : MonoBehaviour
{
    #region Fields
    [SerializeField] private Animator _animator;
    #endregion
    #region Public Fields
    public Action FireAnimationEndsAction;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        var behaviours = _animator.GetBehaviours<EndStateBehaviour>();
        foreach (var b in behaviours)
            b.StateExitAction += HandleAnimationFinished;
    }

    void OnDestroy()
    {
        var behaviours = _animator.GetBehaviours<EndStateBehaviour>();
        foreach (var b in behaviours)
            b.StateExitAction -= HandleAnimationFinished;
    }
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
    #region Private Methods
    private void HandleAnimationFinished(string stateName)
    {
        if(stateName == "FireEditable")
        {
            FireAnimationEndsAction?.Invoke();
        }
    }
    #endregion
}