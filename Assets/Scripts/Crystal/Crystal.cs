using System;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    #region Fields 
    [SerializeField] private int _id;
    [SerializeField] private HeroDetector _heroDetector;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject _radarSymbolGO;    
    [SerializeField] private SfxManager _sfxManager;
    #endregion

    #region Private Fields
    #endregion

    #region Public Fields
    public Action<int,int> CrystalTouchedAction;
    #endregion    

    #region Unity Callbacks
    private void Start()
    {
        _heroDetector.detectedAction += HandleDetected;
    }

    private void OnDestroy()
    {
        _heroDetector.detectedAction -= HandleDetected;
    }
    #endregion

    #region Public Mthods
    public void Hide()
    {
        _meshRenderer.enabled = false;
        _radarSymbolGO.SetActive(false);
        _heroDetector.detectedAction -= HandleDetected;
    }
    #endregion
    
    #region Private Methods
    private void HandleDetected(GameObject hero)
    {
        _sfxManager.PlayClip1();
        int heroId = hero.GetComponent<HeroBuddy>().GetId();
        CrystalTouchedAction?.Invoke(_id,heroId);
        _heroDetector.detectedAction -= HandleDetected;
    }
    #endregion
    #region Public Methods

    #endregion
}