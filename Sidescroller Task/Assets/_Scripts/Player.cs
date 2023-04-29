using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// TODO: Constrain position to not leave screen, not necessary but nice to have

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    
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
        _speed = maxSpeed;
    }
    
    private void Update()
    {
        if (!_alive) return;
         
        GetInput();
        
        MoveInDirection(_speed, _moveDirection);
    }

    private void GetInput()
    {
        _moveDirection = (_mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized.x;
    }
    
    private void MoveInDirection(float speed, float direction)
    {
        transform.Translate(direction * speed * Time.deltaTime,0f,0f);
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
