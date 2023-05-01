using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Blast : PowerUp
{
    [SerializeField] private float blastRange;
    [SerializeField] private float blastSpeed;

    public override bool OnUse()
    {
        if (!base.OnUse()) return false;
        
        collider2D.radius = 0.1f;
        renderer.transform.localScale = new Vector2(0.2f, 0.2f);

        return true;
    }

    public override void Progress(float time)
    {
        if (!IsActive) return;
        
        var range = time * blastSpeed;
        if (collider2D.radius + range <= blastRange)
        {
            collider2D.radius += range;
            range *= 2; // local scale is a diameter co it's radius increase * 2
            renderer.transform.localScale += new Vector3(range, range, 1f);
        }
        else
        {
            End();
        }
    }
}