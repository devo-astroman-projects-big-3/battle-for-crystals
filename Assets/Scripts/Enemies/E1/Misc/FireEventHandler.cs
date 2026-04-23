using UnityEngine;
using System;

public class FireEventHandler : MonoBehaviour
{
    #region Fields
    #endregion
    #region Public Fields
    public Action MiddleFireEventAction;
    #endregion

    #region Unity Callbacks
    #endregion
    #region Public Methods    
    public void OnMiddleFire()
    {
        MiddleFireEventAction?.Invoke();
    }
    #endregion
}