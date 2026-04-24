using UnityEngine;
using System;

public class EObjectCollider : MonoBehaviour, IDamageable
{
    #region Fields    
    #endregion

    #region Private Fields
    #endregion

    #region Public Fields
    public Action<int> ReceiveHitAction;
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void TakeDamage(int amount)
    {
        ReceiveHitAction?.Invoke(amount);
    }
    #endregion

    #region Private Methods
    #endregion
}