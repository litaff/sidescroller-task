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
    [SerializeField] private float positionXDelta;
    
    private PowerUps _powerUps;
    private Vector2 _startingPosition;
    private bool _alive;
    private float _moveDirection;
    private float _speed;
    private Camera _mainCam;

    public static event Action OnDeath;
    
    private void Awake()
    {
        _startingPosition = transform.position;
        timeToMaxSpeed += 0.001f; // make sure it's not 0
        _mainCam = Camera.main;
        _powerUps = GetComponentInChildren<PowerUps>();
        GameManager.OnLaunch += ReadyLaunch;
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
    
    private void Update()
    {
        if (!_alive) return;
        
        if (_speed < maxSpeed) SpeedUp(Time.deltaTime);

        GetInput();
        
        MoveInDirection(_speed, _moveDirection);
        _powerUps.UpdatePowerUps();
    }

    private void SpeedUp(float time)
    {
        _speed += maxSpeed/timeToMaxSpeed * time;
        _speed = Mathf.Clamp(_speed, 0f, maxSpeed); // prevent from going over
    }
    
    private void GetInput()
    {
        _moveDirection = (_mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized.x;
    }
    
    private void MoveInDirection(float speed, float direction)
    {
        transform.Translate(direction * speed * Time.deltaTime,0f,0f);

        Vector2 position = transform.position;
        if (position.x > positionXDelta || position.x < -positionXDelta)
        {
            transform.position = new Vector2(
                Mathf.Clamp(position.x, -positionXDelta, positionXDelta), 
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
        OnDeath?.Invoke();
    }
}
