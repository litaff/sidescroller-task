using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

// TODO: Differ speed by percentage of asteroid speed

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float delayToStart;
    [SerializeField] private SpawnPositionManager spawnPositionManager;
    
    private bool _spawnAsteroids;
    private float _timeToSpawns;
    private List<Asteroid> _asteroids;

    private void Awake()
    {
        _spawnAsteroids = false;
        _asteroids = new List<Asteroid>();
        PowerUps.OnAsteroidCollision += DestroyAsteroid;
        GameManager.OnLaunch += ClearAsteroids;
        GameManager.OnPlay += StartSpawningWithDelay;
        GameManager.OnLose += StopSpawning;
    }
    
    private void Update()
    {
        if (!_spawnAsteroids) return;
        
        RandomAsteroidRain(Time.deltaTime);
        foreach (var asteroid in _asteroids.Where(asteroid => asteroid))
        {
            MoveAsteroid(asteroid);
        }
    }

    private void ClearAsteroids()
    {
        while (_asteroids.Count > 0)
        {
            var toDestroy = _asteroids[0];
            _asteroids.RemoveAt(0);
            DestroyAsteroid(toDestroy);
        }
    }
    
    private void StartSpawningWithDelay()
    {
        Invoke(nameof(StartSpawning), delayToStart);
        
    }

    void StartSpawning()
    {
        _spawnAsteroids = true;
    }

    void StopSpawning()
    {
        _spawnAsteroids = false;
    }

    private void MoveAsteroid(Asteroid asteroid)
    {
        asteroid.transform.Translate(Vector2.down * (asteroid.Speed * Time.deltaTime));
    }
    
    private void RandomAsteroidRain(float deltaTime)
    {
        if (_timeToSpawns <= 0)
        {
            // random pos around origin
            SpawnAsteroid(
                spawnPositionManager.GetRandomPosition(), 
                Random.Range(minSpeed, maxSpeed));
            // random time till next spawn
            _timeToSpawns = Random.Range(minTimeToSpawn,maxTimeToSpawn);
        }
        else
        {
            _timeToSpawns -= deltaTime;
        }
    }
    
    private void SpawnAsteroid(Vector2 position, float speed)
    {
        var asteroid = Instantiate(
            asteroidPrefab.gameObject,
            position,
            Quaternion.identity,
            transform).GetComponent<Asteroid>();
        asteroid.Init(speed);
        
        _asteroids.Add(asteroid);
    }

    private void DestroyAsteroid(Asteroid asteroid)
    {
        _asteroids.Remove(asteroid);
        Destroy(asteroid.gameObject);
    }

    // for kill zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            DestroyAsteroid(other.GetComponent<Asteroid>());
        }
    }
}
