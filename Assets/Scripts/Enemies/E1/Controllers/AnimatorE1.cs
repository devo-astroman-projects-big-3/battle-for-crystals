using System;
using Fusion;
using UnityEngine;

public class AnimatorE1 : NetworkBehaviour
{
    #region Fields
    [SerializeField] private Animator _animator;
    [SerializeField] private NetworkMecanimAnimator  _networkMecanimAnimator;
    [SerializeField] private SkinnedMeshRenderer[]  _skinMeshes;
    #endregion
    #region Public Fields
    public Action FireAnimationEndsAction;
    public Action ReceiveHitAnimationEndsAction;
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
        if (!Object.HasStateAuthority)
            return;

        _networkMecanimAnimator.SetTrigger("HideDown");
    }

    public void PlayIdleShowUp()
    {
        if (!Object.HasStateAuthority)
            return;

        _networkMecanimAnimator.SetTrigger("ShowUp");
    }

    public void PlayFire()
    {
        if (!Object.HasStateAuthority)
            return;

        _networkMecanimAnimator.SetTrigger("Fire");
    }
    public void PlayReceiveDamage()
    {
        if (!Object.HasStateAuthority)
            return;

        _networkMecanimAnimator.SetTrigger("ReceiveDamage");
    }

    public void HideSkin()
    {
        for(int i = 0; i<_skinMeshes.Length; i++)
        {
            _skinMeshes[i].enabled = false;
        }
    }

    public void ShowSkin()
    {
        for(int i = 0; i<_skinMeshes.Length; i++)
        {
            _skinMeshes[i].enabled = true;
        }
    }
    #endregion
    #region Private Methods
    private void HandleAnimationFinished(string stateName)
    {
        if(stateName == "FireEditable")
        {
            FireAnimationEndsAction?.Invoke();
        }else if (stateName == "ReceiveDamage")
        {
            ReceiveHitAnimationEndsAction?.Invoke();
        }
    }
    #endregion
}