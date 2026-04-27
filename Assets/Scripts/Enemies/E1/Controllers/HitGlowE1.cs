using UnityEngine;

public class HitGlowE1 : MonoBehaviour
{
    #region Fields
    [SerializeField] private GlowFlickerController[] _glowFlickerController;
    #endregion
    #region Public Fields    
    #endregion

    #region Unity Callbacks
    #endregion
    #region Public Methods
    public void MakeGlowHitOn()
    {
        
        for(int i = 0; i < _glowFlickerController.Length; i++)
        {
            _glowFlickerController[i].StartFlicker(2);
        }

    }

    public void MakeGlowHitOff()
    {
        for(int i = 0; i < _glowFlickerController.Length; i++)
        {
            _glowFlickerController[i].StopFlicker();
        }        
    }
    #endregion
    #region Private Methods    
    #endregion
}