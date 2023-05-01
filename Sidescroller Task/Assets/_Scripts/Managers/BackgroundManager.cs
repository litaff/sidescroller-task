using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Color launchColor;
    [SerializeField] private Color playColor;
    [SerializeField] private float transitionTime;

    private float _transition; // 0 - launch 1 - play
    private bool _play;
    private Camera _camera;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _camera = Camera.main;
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
        _transition = 0; // start in menu
        GameManager.OnPlay += OnPlay;
        GameManager.OnLose += OnMenu;
    }

    private void Update()
    {
        Transition();
        
        if (_play)
        {
            _transition += 1 / transitionTime * Time.deltaTime;
            if (_transition >= .9f)
            {
                _particleSystem.Play();
            }
        }
        else
        {
            _transition -= 1 / (transitionTime * 2) * Time.deltaTime; // extend for lose screen
            _particleSystem.Pause();
            StartCoroutine(ClearAfter(transitionTime));
        }

        _transition = Mathf.Clamp01(_transition);
        
        
    }

    private void Transition()
    {
        _camera.backgroundColor = Color.Lerp(launchColor, playColor, _transition);
    }

    private void OnPlay()
    {
        _play = true;
    }

    private void OnMenu()
    {
        _play = false;
    }

    private IEnumerator ClearAfter(float time)
    {
        yield return new WaitForSeconds(time);
        
        _particleSystem.Clear();
    }
}
