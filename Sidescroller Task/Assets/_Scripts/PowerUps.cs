using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float shieldDuration;
    [SerializeField] private Collider2D shieldCollider;
    [SerializeField] private SpriteRenderer shieldRenderer;
    private Shield _shield;

    public static Action<Asteroid> OnAsteroidCollision;

    private void Awake()
    {
        _shield = new Shield(shieldDuration, shieldCollider, shieldRenderer);
    }

    private void Update()
    {
        _shield.Expire(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha1)) UseShield();
    }

    private bool UseShield()
    {
        return _shield.OnUse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_shield.IsActive)
        {
            if (other.CompareTag("Asteroid"))
            {
                OnAsteroidCollision?.Invoke(other.GetComponent<Asteroid>());
            }
        }
    }
}
