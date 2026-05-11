using UnityEngine;
using TMPro;

public class CrystalsUI : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private TMP_Text _crystalsText;
    #endregion

    #region Public Methods
    public void SetCrystals(int nCrystals)
    {
        _crystalsText.text = $"Crystals x{nCrystals}";
    }

    #endregion
}
