using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCharge : MonoBehaviour
{
    [SerializeField] private ChargeType type;
    private SpriteRenderer _renderer;
    private CircleCollider2D _collider2D;
    private AudioSource _audioSource;
    private bool _charged;
    
    public static Action<ChargeType> OnChargePickUp;

    private void Awake()
    {
        _charged = true;
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || ! _charged) return;
        OnChargePickUp?.Invoke(type);
        _audioSource.Play();
        // disable without destroying, manager will take care of that
        _renderer.enabled = false;
        _charged = false;
    }
}
