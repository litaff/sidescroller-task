using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCharge : MonoBehaviour
{
    [SerializeField] private ChargeType type;
    private SpriteRenderer _renderer;
    private CircleCollider2D _collider2D;
    
    public static Action<ChargeType> OnChargePickUp;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        OnChargePickUp?.Invoke(type);
        // disable without destroying, manager will take care of that
        _renderer.enabled = false;
        _collider2D.enabled = false;
    }
}
