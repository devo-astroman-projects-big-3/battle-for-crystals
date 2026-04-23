using System;
using UnityEngine;

public class E1 : MonoBehaviour
{
    #region Fields
    [SerializeField] private AnimatorE1 _animatorE1;
    [SerializeField] private PlayerRadarE1 _playerRadarE1;
    [SerializeField] private Transform _rootHeadTransform;
    private Transform _target;
    #endregion
    #region Public Fields
    public Action<GameObject> PlayerDetectedAction;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _playerRadarE1.PlayerDetectedAction += HandlePlayerDetected;
    }

    void OnDestroy()
    {
        _playerRadarE1.PlayerDetectedAction -= HandlePlayerDetected;
    }
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
        Debug.Log("Fire!!!!");
        _animatorE1.PlayFire();
    }

    public void ReceiveDamage()
    {
        _animatorE1.PlayReceiveDamage();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void LookAtTarget()
    {
        _rootHeadTransform.LookAt(_target);
    }
    #endregion
    #region Private Methods
    private void HandlePlayerDetected(GameObject playerDetected)
    {
        PlayerDetectedAction?.Invoke(playerDetected);
    }
    #endregion
}