using UnityEngine;
using System;


public class MainHero : MonoBehaviour
{
    #region Fields
    [SerializeField] private HeroMovement _heroMovement;
    [SerializeField] private HeroBuddy _heroBuddy;
    [SerializeField] AnimatorH1 _animatorH1;
    [SerializeField] HitGlowE1 _hitGlowE1;
    [SerializeField] HeroTagLayerManager _heroTagLayerManager;
    [SerializeField] HeroTeleporter _heroTeleporter;

    #endregion
    #region Private Fields
    private int _health = 5;
    private Vector3 _lastCheckpoint;
    #endregion

    #region Public Fields
    public Action<int> ReceiveHitAction;
    public Action ReceiveHitAnimationEndsAction;
    #endregion


    #region Unity Callbacks
    void Start()
    {
        _heroBuddy.ReceiveHitAction += HandleReceiveHit;
        _animatorH1.ReceiveHitAnimationEndsAction += HandleReceiveHitAnimationEnds;
    }
    void OnDestroy()
    {
        _heroBuddy.ReceiveHitAction -= HandleReceiveHit;
        _animatorH1.ReceiveHitAnimationEndsAction -= HandleReceiveHitAnimationEnds;
    }
    #endregion

    #region Public Methods
    public void BlockInputs()
    {
        _heroMovement.BlockInputs();
    }

    public void UnblockInputs()
    {
        _heroMovement.UnblockInputs();
    }

    public void PlayReceiveHit()
    {
        _animatorH1.PlayReceiveHit();
    }

    public int GetHealth()
    {
        return _health;
    }

    public void DisableCollider()
    {
        CapsuleCollider c = _heroBuddy.GetComponent<CapsuleCollider>();
        c.enabled = false;
    }

    public void EnableCollider()
    {
        CapsuleCollider c = _heroBuddy.GetComponent<CapsuleCollider>();
        c.enabled = true;
    }
    #endregion
    #region Private Methods
    public void HandleReceiveHit(int damage)
    {
        ReceiveHitAction?.Invoke(damage);
    }

    public void HandleReceiveHitAnimationEnds()
    {
        ReceiveHitAnimationEndsAction?.Invoke();
    }

    public void ShowHitFlickerGlow()
    {   
        _hitGlowE1.MakeGlowHitOn();
    }

    public void StopHitFlickerGlow()
    {   
        _hitGlowE1.MakeGlowHitOff();
    }

    public void HideSkin()
    {   
        _animatorH1.HideSkin();
    }
    public void ShowSkin()
    {   
        _animatorH1.ShowSkin();
    }

    public void PlaceAtCheckpoint()
    {
        _heroTeleporter.Teleport(_lastCheckpoint);
    }

    public void SetLastCheckpoint(Vector3 lastPosition)
    {
        _lastCheckpoint = lastPosition;
    }

    public void ChangeTagAndLayerToDead()
    {
        _heroTagLayerManager.ChangeToDeadTag();
        _heroTagLayerManager.ChangeToDeadLayer();
    }

    public void ChangeTagAndLayerToPlayer()
    {
        _heroTagLayerManager.ChangeToPlayerTag();
        _heroTagLayerManager.ChangeToPlayerLayer();
    }
    #endregion
}
