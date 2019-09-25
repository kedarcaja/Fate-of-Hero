using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Timer
{
    public event Action OnStart, OnUpdate, OnStop, OnPause;
    public int ElapsedTimeI { get; private set; }
    public float ElapsedTimeF { get; private set; }
    public bool IsStopped { get; private set; }
    public bool IsPaused { get; private set; }
    private float updateValue;
    private float delay;
    private float currentTime;
    private MonoBehaviour starter;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateValue">value which timer increments per delay</param>
    /// <param name="delay">timer waits for this value</param>
    ///  <param name="starter">set MonoBehaviour to start coroutines</param>
    public _Timer(float updateValue, float delay, MonoBehaviour starter)
    {
        SetTimer(updateValue, delay);
        this.starter = starter;
    }
    /// <summary>
    /// To set timer
    /// </summary>
    /// <param name="updateValue"></param>
    /// <param name="delay"></param>
    public void SetTimer(float updateValue, float delay)
    {
        this.updateValue = updateValue;
        this.delay = delay;
    }
    /// <summary>
    /// Starts timer counting
    /// </summary>
    public void Execute()
    {
        if (IsRunning())
        {
            if (IsStopped)
            {
                Reset();
            }
            IsStopped = false;
            IsPaused = false;
            OnStart?.Invoke();
            starter.StartCoroutine(Run());
        }

    }
    /// <summary>
    /// Stops timer counting and resets time
    /// </summary>
    public void Stop()
    {
        if (IsRunning())
        {
            IsStopped = true;
            starter.StopCoroutine(Run());

            OnStop?.Invoke();
        }
    }
    /// <summary>
    /// Pauses timer
    /// time is not restored
    /// </summary>
    public void Pause()
    {
        if (IsRunning())
        {
            IsPaused = true;

            OnPause?.Invoke();

            starter.StopCoroutine(Run());
        }
    }
  
    private void Update()
    {
        currentTime += updateValue;
        ElapsedTimeF = currentTime;
        ElapsedTimeI = (int)currentTime;

        OnUpdate?.Invoke();
    }
    /// <summary>
    /// Resets timer
    /// </summary>
    /// <param name="continu"></param>
    private void Reset()
    {
        currentTime = 0;
        ElapsedTimeF = 0;
        ElapsedTimeI = 0;
    }
    private IEnumerator Run()
    {

        while (IsRunning())
        {
            yield return new WaitForSeconds(delay);
            Update();
        }
    }
    /// <summary>
    /// Returns if timer is not stopped and is not paused
    /// </summary>
    /// <returns></returns>
    public bool IsRunning()
    {
        return !IsPaused && !IsStopped;
    }
}
