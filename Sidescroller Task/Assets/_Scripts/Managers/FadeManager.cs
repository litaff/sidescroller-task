using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private RawImage screenFill;
    [SerializeField] private Fade onLoseFade;
    [SerializeField] private float unFadeTime;

    private bool _fading;
    private float _timeToFade;
    private float _fadeTimer;
    private float _fadeFrom;
    private float _fadeTo; // alpha is max 1 fo 1 - _fadeFrom is always the opposite max value

    public static Action OnFadingStart;
    public static Action OnFadingEnd;
    
    private void Awake()
    {
        GameManager.OnLaunch += FadeOnLaunch;
        GameManager.OnLose += FadeOnLose;
    }

    private void Update()
    {
        if (!_fading) return;
        
        Fade(Time.deltaTime);
    }

    private void Fade(float time)
    {
        _fadeTimer += time;
        var alpha = Mathf.Lerp(_fadeFrom, _fadeTo, _fadeTimer/_timeToFade);
        screenFill.color = new Color(
            screenFill.color.r, 
            screenFill.color.g, 
            screenFill.color.b, 
            alpha);
        
        if (!(_fadeTimer / _timeToFade >= 1)) return;
        
        _fading = false;
        OnFadingEnd?.Invoke();
    }

    private void FadeOnLose()
    {
        if (onLoseFade is null) return;
        
        FadeToOver(onLoseFade.color, onLoseFade.time);
    }

    private void FadeOnLaunch()
    {
        if (screenFill.color.a <= 0) return;
        
        FadeFromOver(unFadeTime);
    }
    
    private void FadeToOver(Color color, float time)
    {
        OnFadingStart?.Invoke();
        _fading = true;
        _timeToFade = time;
        _fadeTimer = 0;
        screenFill.color = new Color(color.r, color.g, color.b, 0f);
        _fadeFrom = screenFill.color.a;
        _fadeTo = 1f - _fadeFrom;
    }

    private void FadeFromOver(float time)
    {
        OnFadingStart?.Invoke();
        _fading = true;
        _timeToFade = time;
        _fadeTimer = 0;
        _fadeFrom = screenFill.color.a;
        _fadeTo = 1f - _fadeFrom;
    }
}
