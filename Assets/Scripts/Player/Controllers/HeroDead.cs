using UnityEngine;
using System.Collections;

public class HeroDead : MonoBehaviour
{
    #region Fields    
    [SerializeField] private HeroSkin _heroSkin;
    #endregion

    #region Private Fields
    private Coroutine _deadRoutine;
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void DeadDelay(int secs)
    {
        if (_deadRoutine != null)
            StopCoroutine(_deadRoutine);

        _deadRoutine = StartCoroutine(DeadDelayRoutine(secs));
    }

    public void ApplyDead(){
        _heroSkin.HideSkin();
    }
    #endregion

    #region Private Methods
    private IEnumerator DeadDelayRoutine(int secs)
    {
        yield return new WaitForSeconds(secs);

        ApplyDead();
        _deadRoutine = null;
    }


    #endregion
}
