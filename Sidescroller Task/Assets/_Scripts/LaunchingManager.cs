using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LaunchingManager : MonoBehaviour
{
    [SerializeField] private string launchText;
    [SerializeField] private int launchDuration;
    [SerializeField] private TMP_Text countDownDisplay;
    
    private bool Launching => countDownDisplay.enabled;
    
    private float _timeToLaunch;
    
    public static Action OnLaunchingStart;
    public static Action OnLaunchingEnd;

    private void Awake()
    {
        GameManager.OnLaunching += StartLaunching;
    }

    private void Start()
    {
        countDownDisplay.enabled = false;
    }

    private void Update()
    {
        if (!Launching) return;

        UpdateDisplay();
        
        if (_timeToLaunch > 0)
        {
            if (_timeToLaunch < .9f)
                _timeToLaunch -= Time.deltaTime / 2; // slow down to better display 0 and launch text
            else
                _timeToLaunch -= Time.deltaTime;
            
        }
        else
        {
            EndLaunching();
        }
        
    }

    private void StartLaunching()
    {
        OnLaunchingStart?.Invoke();
        _timeToLaunch = launchDuration + 0.9f; // to display 5 longer
        UpdateDisplay();
        countDownDisplay.enabled = true;
    }

    private void EndLaunching()
    {
        OnLaunchingEnd?.Invoke();
        countDownDisplay.enabled = false;
    }

    private void UpdateDisplay()
    {
        countDownDisplay.text = _timeToLaunch > .5f ? ((int) _timeToLaunch).ToString() : launchText;
    }
}
