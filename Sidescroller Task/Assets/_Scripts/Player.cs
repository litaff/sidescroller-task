using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;

    private float _moveDirection;
    private float _speed;
    private Camera _mainCam;

    public event Action<Vector2> OnDeath;
    
    private void Awake()
    {
        _speed = maxSpeed;
        _mainCam = Camera.main;
    }

    private void Update()
    {
        _moveDirection = (_mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized.x;
        
        MoveInDirection(_speed, _moveDirection);
        
    }

    private void MoveInDirection(float speed, float direction)
    {
        transform.Translate(direction * speed * Time.deltaTime,0f,0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            OnDeath?.Invoke(new Vector2()); // put collision source direction here
        }
    }
}
