using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// works fine, but not perfect
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip play;
    [SerializeField] private AudioClip menu;
    [SerializeField] private float menuTransitionTime;
    [SerializeField] private float playTransitionTime;

    private AudioClip _audioTarget;
    private float _transitionTime;
    private bool _transition;
    private bool _fadingOut;
    private bool _fadingIn;
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.volume = 0;
        OnMenu();
        GameManager.OnLaunching += OnPlay;
        GameManager.OnLose += OnMenu;
    }

    private void Update()
    {
        if (!_transition) return;

        if (_fadingOut)
        {
            DecreaseVolumeToOver(0, _transitionTime, Time.deltaTime);
            return;
        }

        if (_fadingIn)
        {
            IncreaseVolumeToOver(1, _transitionTime, Time.deltaTime);
            return;
        }

        EndTransition();
    }

    private void OnMenu()
    {
        StartTransition();
        _source.loop = true;
        _audioTarget = menu;
        _transitionTime = menuTransitionTime;
    }

    private void OnPlay()
    {
        StartTransition();
        _audioTarget = play;
        _transitionTime = playTransitionTime;
    }

    private void StartTransition()
    {
        _transition = true;
        _fadingOut = true;
        _fadingIn = false;
    }

    private void EndTransition()
    {
        _source.clip = _audioTarget;
        _source.Play();
        if (_source.clip == play)
        {
            StartCoroutine(PlayMusicOnLoop(67.845f)); // 67.845 is the exact moment to loop to
        }
        _fadingIn = true;
    }
    
    private void IncreaseVolumeToOver(float target, float transition, float time)
    {
        _source.volume += 1 / transition * time;
        if (_source.volume >= target)
        {
            _fadingIn = false;
            _transition = false;
        }
    }
    
    private void DecreaseVolumeToOver(float target, float transition, float time)
    {
        _source.volume -=  1 / transition * time;
        if (_source.volume <= target)
            _fadingOut = false;
    }

    private IEnumerator PlayMusicOnLoop (float endIntro)
    {
        _source.loop = false;
        var withoutIntro = _source.clip.length - endIntro;
        yield return new WaitForSeconds (endIntro);
        var t = _source.time;
        yield return new WaitForSeconds (withoutIntro);
        while (_source.clip == play)
        {
            _source.time = t;
            _source.Play ();
            yield return new WaitForSeconds (withoutIntro);
        }
    }
}
