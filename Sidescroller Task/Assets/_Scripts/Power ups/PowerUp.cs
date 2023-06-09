﻿using System;
using UnityEngine;

[Serializable]
public abstract class PowerUp
{
    [SerializeField] protected CircleCollider2D collider2D;
    [SerializeField] protected ParticleSystem particleSystem;
    [SerializeField] private AudioSource soundEffect;
    private int _charges;
    
    
    public static Action OnEnd;
    public bool IsActive { get; protected set; }

    public virtual void Init()
    {
        if (collider2D) collider2D.enabled = false;
        _charges = 1;
        IsActive = false;
    }
    
    /// <summary>
    /// Invoke this before power up logic
    /// </summary>
    public virtual bool OnUse()
    {
        if (_charges <= 0 || !collider2D || IsActive) return false;

        _charges--;
        collider2D.enabled = true;
        particleSystem.Play();
        IsActive = true;
        soundEffect.Play();

        return true;
    }

    public virtual void Progress(float time)
    {
    }

    public virtual void End()
    {
        IsActive = false;
        collider2D.enabled = false;
        OnEnd?.Invoke();
    }
    
    public void GainCharge()
    {
        _charges++;
    }

    public int GetCharges()
    {
        return _charges;
    }
}