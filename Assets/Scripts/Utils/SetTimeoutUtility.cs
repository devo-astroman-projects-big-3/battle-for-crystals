using System;
using System.Collections;
using UnityEngine;

public class SetTimeoutUtility : IDisposable
{
    private MonoBehaviour context;
    private Coroutine runningCoroutine;

    private bool disposed = false;
    private bool paused = false;

    private Action storedCallback;

    private float totalDelay;
    private float remainingDelay;
    private float startTime;

    public SetTimeoutUtility(MonoBehaviour c)
    {
        context = c;
    }

    public void SetTimeout(Action fn, float delay)
    {
        if (disposed)
        {
            Debug.LogWarning("SetTimeoutUtility: Attempted to use after Dispose(). Ignored.");
            return;
        }

        StopTimeout();

        storedCallback = fn;

        totalDelay = delay;
        remainingDelay = delay;
        paused = false;

        StartTimer(remainingDelay);
    }

    private void StartTimer(float delay)
    {
        startTime = Time.time;
        runningCoroutine = context.StartCoroutine(_SetTimeout(delay));
    }

    private IEnumerator _SetTimeout(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!disposed && !paused)
        {
            storedCallback?.Invoke();
        }

        runningCoroutine = null;
    }

    // -------------------------
    // Pause
    // -------------------------

    public void Pause()
    {
        if (paused || runningCoroutine == null)
            return;

        float elapsed = Time.time - startTime;
        remainingDelay -= elapsed;

        StopTimeout();

        paused = true;
    }

    // -------------------------
    // Resume
    // -------------------------

    public void Resume()
    {
        if (!paused || disposed)
            return;

        paused = false;

        StartTimer(remainingDelay);
    }

    // -------------------------
    // Reset
    // -------------------------

    public void Reset()
    {
        if (disposed)
            return;

        StopTimeout();

        paused = false;
        remainingDelay = totalDelay;

        if (storedCallback != null)
        {
            StartTimer(totalDelay);
        }
    }

    // -------------------------
    // Stop
    // -------------------------

    public void StopTimeout()
    {
        if (runningCoroutine != null && context != null)
        {
            context.StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }

    // -------------------------
    // Dispose
    // -------------------------

    public void Dispose()
    {
        if (disposed)
            return;

        StopTimeout();

        context = null;
        storedCallback = null;

        disposed = true;
    }
}