using UnityEngine;


public class MainHero : MonoBehaviour
{
    #region Fields
    [SerializeField] private HeroMovement _heroMovement;
    #endregion

    #region Private Fields
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void BlockInputs()
    {
        _heroMovement.BlockInputs();
    }

    public void UnblockInputs()
    {
        _heroMovement.UnblockInputs();
    }
    #endregion
}
