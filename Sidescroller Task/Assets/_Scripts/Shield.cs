using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Shield : PowerUp
{
    private float _timeLeft;
    private readonly float _duration;
    private readonly Collider2D _shieldCollider2D;
    private readonly SpriteRenderer _spriteRenderer;

    public static Action OnExpire;
    
    public bool IsActive { get; private set; }

    public Shield(float duration, Collider2D collider, SpriteRenderer spriteRenderer) : base()
    {
        _duration = duration;
        _shieldCollider2D = collider;
        _shieldCollider2D.enabled = false;
        _spriteRenderer = spriteRenderer;
        _spriteRenderer.enabled = false;
        IsActive = false;
    }

    public override bool OnUse()
    {
        if (!base.OnUse() || !_shieldCollider2D || !_spriteRenderer || IsActive) return false;

        IsActive = true;
        _timeLeft = _duration;
        _shieldCollider2D.enabled = true;
        _spriteRenderer.enabled = true;
        
        return true;
    }

    public void Expire(float time)
    {
        if (!IsActive) return;
        
        if (_timeLeft <= 0)
        {
            IsActive = false;
            _shieldCollider2D.enabled = false;
            _spriteRenderer.enabled = false;
            OnExpire?.Invoke();
        }
        else
        {
            _timeLeft -= time;
        }
    }
}