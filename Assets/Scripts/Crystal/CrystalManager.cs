using System;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    #region Fields 
    [SerializeField] private Crystal[] _crystals;
    [SerializeField] private StateNetworkCrystal _stateNetworkCrystal;    
    #endregion

    #region Private Fields
    #endregion

    #region Public Fields
    public Action CrystalsChangedAction;
    #endregion



    #region Unity Callbacks
    private void Start()
    {
        if (_crystals != null)
        {
            for (int i = 0; i < _crystals.Length; i++)
            {
                _crystals[i].CrystalTouchedAction += HandleCrystalTouched;
            }
        }
        
        _stateNetworkCrystal.CrystalsChangedAction += HandleCrystalsChanged;

    }

    private void OnDestroy()
    {
        for (int i = 0; i < _crystals.Length; i++)
        {
            if (_crystals[i] != null)
                _crystals[i].CrystalTouchedAction -= HandleCrystalTouched;
        }

        _stateNetworkCrystal.CrystalsChangedAction -= HandleCrystalsChanged;
    }
    #endregion

    #region Private Methods

    private void HandleCrystalTouched(int crystalId, int heroId)
    {
        _stateNetworkCrystal.RequestCrystalTaken(crystalId,heroId);
    }

    private void HandleCrystalsChanged()
    {
        int[] allCrystalsData = _stateNetworkCrystal.GetAllCrystalsData();

        for (int i = 0; i < allCrystalsData.Length; i++)
        {
            if (allCrystalsData[i] != -1)
                _crystals[i].Hide();
        }

        CrystalsChangedAction?.Invoke();

    }
    #endregion
    #region Public Methods
    public int[] GetCrystalStates()
    {
        return _stateNetworkCrystal.GetAllCrystalsData();
    }

    #endregion
}
