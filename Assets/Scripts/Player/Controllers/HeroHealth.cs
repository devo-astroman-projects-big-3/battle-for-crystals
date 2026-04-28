using UnityEngine;
using System.Collections;

public class HeroHealth : MonoBehaviour
{
    #region Fields    
    [SerializeField] private int _maxLife = 10;
    #endregion

    #region Private Fields

    private int _life = 10;

    private bool _ignoreHurt = false;
    private Coroutine _ignoreRoutine;

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
        if (_ignoreHurt)
            return;

        Life -= amount;
    }

    /// <summary>
    /// Makes the hero immune to damage for X seconds.
    /// </summary>
    public void IgnoreHurtForSecs(int seconds)
    {
        if (_ignoreRoutine != null)
            StopCoroutine(_ignoreRoutine);

        _ignoreRoutine = StartCoroutine(IgnoreHurtRoutine(seconds));
    }

    #endregion

    #region Private Methods

    private IEnumerator IgnoreHurtRoutine(int seconds)
    {
        _ignoreHurt = true;

        yield return new WaitForSeconds(seconds);

        _ignoreHurt = false;
        _ignoreRoutine = null;
    }

    #endregion
}