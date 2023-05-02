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
    [SerializeField] private SpawnPositionManager spawnPositionManager;

    private float _transition; // 0 - launch 1 - play
    private bool _play;
    private Camera _camera;
    private ParticleSystem _particleSystem;
    private Player _player;
    private SpriteRenderer _renderer;
    private Vector2 _initialPlatformPosition;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _initialPlatformPosition = _renderer.transform.position;
        _camera = Camera.main;
        _particleSystem = GetComponent<ParticleSystem>();
        var shape = _particleSystem.shape;
        shape.radius = spawnPositionManager.GetDeltaX();
        _particleSystem.Stop();
        _transition = 0; // start in menu
        GameManager.OnLaunch += OnLaunch;
        GameManager.OnPlay += OnPlay;
        GameManager.OnLose += OnMenu;
    }

    private void Update()
    {
        Transition();
        
        if (_play)
        {
            if (_renderer.transform.position.y > -10f)
            {
                _renderer.transform.Translate(Vector2.down * (_player.GetSpeed() * Time.deltaTime / 2));
            }
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

    private void OnLaunch()
    {
        _renderer.transform.position = _initialPlatformPosition;
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
