using System;
using UnityEngine;

public class E1 : MonoBehaviour
{
    #region Fields
    [SerializeField] private AnimatorE1 _animatorE1;
    [SerializeField] private PlayerRadarE1 _playerRadarE1;
    [SerializeField] private Transform _rootHeadTransform;
    [SerializeField] private FireEventHandler _fireEventHandler;
    [SerializeField] private EnemyFireShoot _enemyFireShoot;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private EObjectCollider _eObjectCollider;
    [SerializeField] private HealthE1 _healthE1;
    
    private Transform _target;
    #endregion
    #region Public Fields
    public Action<GameObject> PlayerDetectedAction;
    public Action PlayerUndetectedAction;
    
    public Action MiddleFireEventAction;
    public Action FireAnimationEndsAction;
    public Action ReceiveHitAnimationEndsAction;
    public Action<int> ReceiveHitAction;
    
    
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _playerRadarE1.PlayerDetectedAction += HandlePlayerDetected;
        _playerRadarE1.PlayerUndetectedAction += HandlePlayerUndetected;
        _fireEventHandler.MiddleFireEventAction += HandleMiddleFireEvent;
        _animatorE1.FireAnimationEndsAction += HandleFireAnimationEnds;
        _animatorE1.ReceiveHitAnimationEndsAction += HandleReceiveHitAnimationEnds;

        
        _eObjectCollider.ReceiveHitAction += HandleReceiveHit;
        
        
    }

    void OnDestroy()
    {
        _playerRadarE1.PlayerDetectedAction -= HandlePlayerDetected;
        _playerRadarE1.PlayerUndetectedAction -= HandlePlayerUndetected;
        _fireEventHandler.MiddleFireEventAction -= HandleMiddleFireEvent;
        _animatorE1.FireAnimationEndsAction -= HandleFireAnimationEnds;
        _animatorE1.ReceiveHitAnimationEndsAction -= HandleReceiveHitAnimationEnds;
        _eObjectCollider.ReceiveHitAction -= HandleReceiveHit;
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
        _animatorE1.PlayFire();
    }

    public void ReceiveDamage(int amount)
    {
        _animatorE1.PlayReceiveDamage();
        _healthE1.DealDamageRpc(amount);        
    }

    public int GetLife()
    {   
        return _healthE1.NetworkedHealth;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void LookAtTarget()
    {
        _rootHeadTransform.LookAt(_target);
    }
    public bool IsTargetNull()
    {
        return _target == null;
    }

    public void FireBullet()
    {
        Debug.DrawRay(_muzzle.position, (_target.position - _muzzle.position).normalized * 3f, Color.yellow, 1f);
        _enemyFireShoot.Fire(_muzzle.position, _target.position - _muzzle.position);
    }
    public void Die()
    {   
        Debug.Log("E1 Die");
    }
    #endregion
    #region Private Methods
    private void HandlePlayerDetected(GameObject playerDetected)
    {
        PlayerDetectedAction?.Invoke(playerDetected);
    }

    private void HandlePlayerUndetected()
    {
        PlayerUndetectedAction?.Invoke();
    }



    private void HandleMiddleFireEvent()
    {
        MiddleFireEventAction?.Invoke();
    }

    private void HandleFireAnimationEnds()
    {
        FireAnimationEndsAction?.Invoke();
    }

    private void HandleReceiveHitAnimationEnds()
    {
        ReceiveHitAnimationEndsAction?.Invoke();
    }

    private void HandleReceiveHit(int amount)
    {
        ReceiveHitAction?.Invoke(amount);
    }
    #endregion
}