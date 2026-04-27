using UnityEngine;
using System;

public class PlayerRadarE1 : MonoBehaviour
{
    #region Fields
    [SerializeField] HeroDetector _heroDetector;
    #endregion
    #region Public Fields
    public Action<GameObject> PlayerDetectedAction;
    public Action PlayerUndetectedAction;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _heroDetector.detectedAction += HandleDetectedAction;
        _heroDetector.undetectedAction += HandleUndetectedAction;
    }

    void OnDestroy()
    {
        _heroDetector.detectedAction -= HandleDetectedAction;
        _heroDetector.undetectedAction -= HandleUndetectedAction;
    }
    #endregion
    #region Public Methods
    #endregion

    #region Private Methods
    private void HandleDetectedAction(GameObject playerDetected)
    {
        PlayerDetectedAction?.Invoke(playerDetected);
    }

    private void HandleUndetectedAction()
    {
        PlayerUndetectedAction?.Invoke();
    }
    
    #endregion

    
}