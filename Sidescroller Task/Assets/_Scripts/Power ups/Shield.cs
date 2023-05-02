using System;
using UnityEngine;

[Serializable]
public class Shield : PowerUp
{
    [SerializeField] private float range;
    [SerializeField] private float duration;
    private float _timeLeft;

    public override void Init()
    {
        base.Init();
        var shape = particleSystem.shape;
        shape.radius = range;
        var main = particleSystem.main;
        main.startLifetime = duration;
    }

    public override bool OnUse()
    {
        if (!base.OnUse()) return false;
        
        _timeLeft = duration;
        collider2D.radius = range;

        return true;
    }

    public override void Progress(float time)
    {
        if (!IsActive) return;
        
        if (_timeLeft <= 0)
        {
            End();
        }
        else
        {
            _timeLeft -= time;
        }
    }

    public override void End()
    {
        base.End();
    }
}
