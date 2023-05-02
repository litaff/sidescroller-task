using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int bonus;
    [SerializeField] private AsteroidSize size;
    [SerializeField] private float speed;
    
    private AudioSource _audioSource;
    private CircleCollider2D _collider2D;
    private SpriteRenderer _renderer;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider2D = GetComponent<CircleCollider2D>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deviation"> Speed is multiplied by this </param>
    public void DeviateSpeed(float deviation)
    {
        if (deviation > 0)
        {
            speed *= deviation;
        }
    }

    public AsteroidSize GetSize()
    {
        return size;
    }

    public float GetSpeed()
    {
        return speed;
    }
    
    public int GetBonus()
    {
        return bonus;
    }

    public float Kill(bool byPlayer)
    {
        _renderer.enabled = false;
        
        if (!byPlayer) return 0;
        _collider2D.enabled = false;
        _particleSystem.Play();
        _audioSource.Play();
        return _audioSource.clip.length;
    }
}
