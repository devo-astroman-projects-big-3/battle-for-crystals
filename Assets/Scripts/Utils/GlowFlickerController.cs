using UnityEngine;
using System.Collections;

public class GlowFlickerController : MonoBehaviour
{
    #region Fields

    [Header("References")]
    [SerializeField] private GlowController _glowController;

    [Header("Flicker Settings")]
    [SerializeField] private float _flickerInterval = 0.1f;

    #endregion

    #region Private Fields

    private Coroutine _flickerRoutine;

    #endregion

    #region Public Methods

    /// <summary>
    /// Starts flickering glow for a number of seconds.
    /// </summary>
    public void StartFlicker(float durationSeconds)
    {
        if (_glowController == null)
        {
            Debug.LogWarning("GlowFlickerController: GlowController reference is missing.");
            return;
        }

        if (_flickerRoutine != null)
            StopCoroutine(_flickerRoutine);

        _flickerRoutine = StartCoroutine(FlickerRoutine(durationSeconds));
    }

    /// <summary>
    /// Stops flicker immediately.
    /// </summary>
    public void StopFlicker()
    {
        if (_flickerRoutine != null)
        {
            StopCoroutine(_flickerRoutine);
            _flickerRoutine = null;
        }

        if (_glowController != null)
            _glowController.DisableGlow();
    }

    #endregion

    #region Private Methods

    private IEnumerator FlickerRoutine(float duration)
    {
        float timer = 0f;
        bool glowOn = false;

        while (timer < duration)
        {
            if (glowOn)
                _glowController.DisableGlow();
            else
                _glowController.EnableGlow();

            glowOn = !glowOn;

            yield return new WaitForSeconds(_flickerInterval);

            timer += _flickerInterval;
        }

        _glowController.DisableGlow();
        _flickerRoutine = null;
    }

    #endregion
}