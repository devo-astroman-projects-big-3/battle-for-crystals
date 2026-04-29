using System;
using Fusion;
using UnityEngine;

public class AnimatorH1 : NetworkBehaviour
{
    #region Fields
    [SerializeField] private Animator _animator;
    [SerializeField] private NetworkMecanimAnimator _networkMecanimAnimator;
    [SerializeField] private SkinnedMeshRenderer[] _skinMeshes;
    #endregion

    #region Public Fields
    public Action ReceiveHitAnimationEndsAction;
    public Action AttackAnimationEndsAction;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        var behaviours = _animator.GetBehaviours<EndStateBehaviour>();
        foreach (var b in behaviours)
            b.StateExitAction += HandleAnimationFinished;
    }

    private void OnDestroy()
    {
        var behaviours = _animator.GetBehaviours<EndStateBehaviour>();
        foreach (var b in behaviours)
            b.StateExitAction -= HandleAnimationFinished;
    }
    #endregion

    #region Public Methods
    public void PlayReceiveHit()
    {
        if (!Object.HasStateAuthority)
            return;

        _networkMecanimAnimator.SetTrigger("ReceiveHit");
    }

    public void PlayAttack()
    {
        if (!Object.HasStateAuthority)
            return;

        _networkMecanimAnimator.SetTrigger("Attack");
    }

    public void HideSkin()
    {
        for (int i = 0; i < _skinMeshes.Length; i++)
        {
            _skinMeshes[i].enabled = false;
        }
    }

    public void ShowSkin()
    {
        for (int i = 0; i < _skinMeshes.Length; i++)
        {
            _skinMeshes[i].enabled = true;
        }
    }
    #endregion

    #region Private Methods
    private void HandleAnimationFinished(string stateName)
    {
        if (stateName == "ReceiveHit")
        {
            ReceiveHitAnimationEndsAction?.Invoke();
        }
        else if (stateName == "Attack")
        {
            AttackAnimationEndsAction?.Invoke();
        }
    }
    #endregion
}
