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
    [SerializeField] private HitGlowE1 _hitGlowE1;
    //[SerializeField] private LookAtNetwork _lookAtNetwork;

    
    
    private Transform _target;
    private Vector3 _lookAtPos;
    private int _health = 5;
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

    public void ReceiveDamage()
    {
         _animatorE1.PlayReceiveDamage();
    }

    public int GetLife()
    {   
        return _healthE1.NetworkedHealth;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public Transform GetTarget()
    {
        return _target;
    }

    public void SetLookAtPos(Vector3 lookAtPos)
    {
        _lookAtPos = lookAtPos;
    }

    public void LookAtTarget()
    {
        _rootHeadTransform.LookAt(_lookAtPos);
        //_lookAtNetwork.SetLookPosition(_target.position);
    }
    public void SetTargetNull()
    {
        _target = null;
    }
    public bool IsTargetNull()
    {
        return _target == null;
    }

    public bool IsTargetFar()
    {
        if (_target == null)
            return true;

        return Vector3.Distance(transform.position, _target.position) > 20f;
        //return false;
    }

    public float GetDistance()
    {       
        return Vector3.Distance(transform.position, _target.position);
    }

    public void FireBullet()
    {
        if(!_target)
            return;

        Debug.DrawRay(_muzzle.position, (_target.position - _muzzle.position).normalized * 3f, Color.yellow, 1f);
        _enemyFireShoot.Fire(_muzzle.position, _target.position - _muzzle.position);
    }
    public void Die()
    {   
        Debug.Log("E1 Die");
    }

    public void ShowHitFlickerGlow()
    {   
        _hitGlowE1.MakeGlowHitOn();
    }

    public void StopHitFlickerGlow()
    {   
        _hitGlowE1.MakeGlowHitOff();
    }
    public bool IsDead(int nHitsReceived)
    {   
        return nHitsReceived >= _health;
    } 

    public void HideSkin()
    {   
        _animatorE1.HideSkin();
    } 

    public void ShowSkin()
    {   
        _animatorE1.ShowSkin();
    } 
    

    /* public int SetNHits(int nHits)
    {   
        return _healthE1.NetworkedHealth;
    } */
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
