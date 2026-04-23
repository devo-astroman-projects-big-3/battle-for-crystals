using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    #region Fields    
    [SerializeField] private int _maxLife = 10;
    #endregion

    #region Private Fields
    private int _life = 10;
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public int Life
    {
        get => _life;

        set
        {
            _life = Mathf.Clamp(value, 0, _maxLife);
        }
    }
    public void DecreaseLife(int amount)
    {
       Life -= amount;
    }
    #endregion

    #region Private Methods
    #endregion
}