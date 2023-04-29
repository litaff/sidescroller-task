using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _state;

    public static Action OnLaunch;
    public static Action OnPlay;
    public static Action OnLose;

    private void Awake()
    {
        Player.OnDeath += LoseState;
    }

    private void Start()
    {
        LaunchState();
    }

    private void Update()
    {
        if (_state == GameState.Launch)
        {
            if (Input.anyKeyDown) PlayState();
        }

        if (_state == GameState.Lose)
        {
            if (Input.anyKeyDown) LaunchState();
        }
    }

    private void LaunchState()
    {
        _state = GameState.Launch;
        OnLaunch?.Invoke();
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
}
