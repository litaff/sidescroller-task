using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _state;
    private List<bool> _busy; // if empty nothing is happening

    public static Action OnLaunch;
    public static Action OnLaunching;
    public static Action OnPlay;
    public static Action OnLose;

    private void Awake()
    {
        Player.OnDeath += LoseState;
        FadeManager.OnFadingStart += Busy;
        FadeManager.OnFadingEnd += Free;
        LaunchingManager.OnLaunchingEnd += PlayState;
    }

    private void Start()
    {
        _busy = new List<bool>();
        LaunchState();
    }

    private void Update()
    {
        if (_state == GameState.Launch && NotBusy())
        {
            if (Input.anyKeyDown) LaunchingState();
        }

        if (_state == GameState.Lose && NotBusy())
        {
            if (Input.anyKeyDown) LaunchState();
        }
    }

    private void LaunchState()
    {
        _state = GameState.Launch;
        OnLaunch?.Invoke();
    }

    private void LaunchingState()
    {
        _state = GameState.Launching;
        OnLaunching?.Invoke();
    }
    
    private void PlayState()
    {
        _state = GameState.Play;
        OnPlay?.Invoke();
    }

    private void LoseState()
    {
        _state = GameState.Lose;
        OnLose?.Invoke();
    }

    private bool NotBusy()
    {
        return _busy.Count <= 0;
    }
    
    private void Busy()
    {
        _busy.Add(true);
    }

    private void Free()
    {
        _busy.RemoveAt(0);
    }
}
