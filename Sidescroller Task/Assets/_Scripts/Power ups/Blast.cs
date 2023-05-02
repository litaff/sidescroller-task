using System;
using UnityEngine;

[Serializable]
public class Blast : PowerUp
{
    [SerializeField] private float blastRange;
    [SerializeField] private float blastSpeed;

    [Tooltip("Set starting lifetime to range/speed and speed to speed")]
    public override void Init()
    {
        base.Init();
        var main = particleSystem.main;
        main.startLifetime = blastRange / blastSpeed;
        main.startSpeed = blastSpeed;
    }

    public override bool OnUse()
    {
        if (!base.OnUse()) return false;
        
        collider2D.radius = 0.1f;

        return true;
    }

    public override void Progress(float time)
    {
        if (!IsActive) return;
        
        var range = time * blastSpeed;
        if (collider2D.radius + range <= blastRange)
        {
            collider2D.radius += range;
        }
        else
        {
            End();
        }
    }
}