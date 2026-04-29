using UnityEngine;
using System;


public class MainHero : MonoBehaviour
{
    #region Fields
    [SerializeField] private HeroMovement _heroMovement;
    [SerializeField] private HeroBuddy _heroBuddy;
    #endregion

    #region Public Fields
    public Action<int> ReceiveHitAction;
    #endregion


    #region Unity Callbacks
    void Start()
    {
        _heroBuddy.ReceiveHitAction += HandleReceiveHit;
    }
    void OnDestroy()
    {
        _heroBuddy.ReceiveHitAction -= HandleReceiveHit;
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
    #endregion
     #region Private Methods
    public void HandleReceiveHit(int damage)
    {
        ReceiveHitAction?.Invoke(damage);
    }
    #endregion
}
