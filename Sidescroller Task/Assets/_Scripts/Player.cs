using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// TODO: Constrain position to not leave screen, not necessary but nice to have
// TODO: Speed up on launch to max speed

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float timeToMaxSpeed;
    [SerializeField] private SpawnPositionManager spawnPositionManager;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private AudioSource launchingSound;
    [SerializeField] private ParticleSystem launchingEffect;
    
    private PowerUps _powerUps;
    private Vector2 _startingPosition;
    private bool _alive;
    private float _moveDirection;
    private float _speed;
    private Camera _mainCam;
    private ParticleSystem.MainModule _mainModule;

    public static event Action OnDeath;

    public float GetSpeed()
    {
        return _speed;
    }
    
    private void Awake()
    {
        _startingPosition = transform.position;
        timeToMaxSpeed += 0.001f; // make sure it's not 0
        _mainCam = Camera.main;
        _powerUps = GetComponentInChildren<PowerUps>();
        _speed = 0f;
        _mainModule = launchingEffect.main;
        _mainModule.startSpeed = _speed;
        GameManager.OnLaunch += ReadyLaunch;
        GameManager.OnLaunching += Launching;
        GameManager.OnPlay += Launch;
    }

    private void ReadyLaunch()
    {
        _alive = false;
        transform.position = _startingPosition;
    }
    
    private void Launch()
    {
        _alive = true;
    }

    private void Launching()
    {
        _speed = 0f;
        _mainModule.loop = true;
        _mainModule.startSpeed = _speed;
        launchingSound.volume = 1f;
        launchingSound.Play();
        launchingEffect.Play();
        StartCoroutine(DeceaseVolumeAfterLaunch(timeToMaxSpeed));
    }
    
    private IEnumerator DeceaseVolumeAfterLaunch(float time)
    {
        yield return new WaitForSeconds(time);
        while (launchingSound.volume > 0)
        {
            launchingSound.volume -= Time.deltaTime / time;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (launchingSound.volume <= 0)
        {
            launchingSound.Stop();
        }
    }
    
    private void Update()
    {
        if (!_alive) return;
        
        if (_speed < maxSpeed) SpeedUp(Time.deltaTime);

        if (_speed >= maxSpeed) _mainModule.loop = false;

        GetInput();
        
        MoveInDirection(_speed, _moveDirection);
        _powerUps.UpdatePowerUps();
    }

    private void SpeedUp(float time)
    {
        _speed += maxSpeed/timeToMaxSpeed * time;
        _speed = Mathf.Clamp(_speed, 0f, maxSpeed); // prevent from going over
        _mainModule.startSpeed = _speed;
    }
    
    private void GetInput()
    {
        _moveDirection = (_mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized.x;
    }
    
    private void MoveInDirection(float speed, float direction)
    {
        transform.Translate(direction * speed * Time.deltaTime,0f,0f);

        Vector2 position = transform.position;
        if (position.x > spawnPositionManager.GetDeltaX() || position.x < -spawnPositionManager.GetDeltaX())
        {
            transform.position = new Vector2(
                Mathf.Clamp(
                    position.x,
                    -spawnPositionManager.GetDeltaX(), 
                    spawnPositionManager.GetDeltaX()), 
                position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Die();
        }
    }

    private void Die()
    {
        _alive = false;
        deathEffect.Play();
        deathSound.Play();
        OnDeath?.Invoke();
    }
}
