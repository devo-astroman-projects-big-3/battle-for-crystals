using UnityEngine;
using System;


public class MainHero : MonoBehaviour
{
    #region Fields
    [SerializeField] private HeroMovement _heroMovement;
    [SerializeField] private HeroBuddy _heroBuddy;
    [SerializeField] AnimatorH1 _animatorH1;
    [SerializeField] HitGlowE1 _hitGlowE1;
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

    
    #endregion
}
