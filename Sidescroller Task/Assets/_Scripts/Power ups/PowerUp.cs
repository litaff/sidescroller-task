using System;
using UnityEngine;

[Serializable]
public abstract class PowerUp
{
    
    [SerializeField] protected CircleCollider2D collider2D;
    [SerializeField] protected SpriteRenderer renderer;
    private int _charges;
    
    
    public static Action OnEnd;
    public bool IsActive { get; protected set; }

    public virtual void Init()
    {
        if (collider2D) collider2D.enabled = false;
        if (renderer) renderer.enabled = false;
        IsActive = false;
    }
    
    /// <summary>
    /// Invoke this before power up logic
    /// </summary>
    public virtual bool OnUse()
    {
        if (_charges <= 0 || !collider2D || !renderer || IsActive) return false;

        _charges--;
        collider2D.enabled = true;
        renderer.enabled = true;
        IsActive = true;

        return true;
    }

    public virtual void Progress(float time)
    {
    }

    public virtual void End()
    {
        IsActive = false;
        collider2D.enabled = false;
        renderer.enabled = false;
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