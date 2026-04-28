using UnityEngine;

public class HeroHitGlow : MonoBehaviour
{
    #region Fields
    [SerializeField] private GlowFlickerController[] _glowFlickerController;
    #endregion

    #region Private Fields    
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void MakeGlowHitOn()
    {
        
        for(int i = 0; i < _glowFlickerController.Length; i++)
        {
            _glowFlickerController[i].StartFlicker(1);
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