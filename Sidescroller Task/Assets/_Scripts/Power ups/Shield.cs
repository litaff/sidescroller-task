using System;
using UnityEngine;

[Serializable]
public class Shield : PowerUp
{
    [SerializeField] private float range;
    [SerializeField] private float duration;
    [Range(0,1)] [SerializeField] private float whenToFlicker;
    [SerializeField] private float flickerRate;
    [SerializeField] protected SpriteRenderer renderer;
    private float _timeLeft;
    private float _flickerTimer;

    public override bool OnUse()
    {
        if (!base.OnUse() || !renderer) return false;
        
        if (renderer) renderer.enabled = false;
        _timeLeft = duration;
        collider2D.radius = range;
        renderer.enabled = true;

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

            if (_timeLeft / duration < whenToFlicker)
            {
                Flicker(time);
            }
        }
    }

    private void Flicker(float time)
    {
        if (_flickerTimer <= 0)
        {
            renderer.enabled = !renderer.enabled;
            _flickerTimer = 1f / flickerRate;
        }
        else
        {
            _flickerTimer -= time;
        }
    }

    public override void End()
    {
        base.End();
        renderer.enabled = false;
    }
}
