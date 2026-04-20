using UnityEngine;

public class WObject : MonoBehaviour, IDamageable
{
    #region Fields    
    #endregion

    #region Private Fields
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void TakeDamage(int amount)
    {
        Debug.Log("___TakeDamage____" + amount);
    }
    #endregion

    #region Private Methods
    #endregion
}