using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private List<Asteroid> asteroidPrefabs;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float speedDeviation;
    [SerializeField] private float chanceToSpawnLarger;
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
        asteroid.transform.Translate(Vector2.down * (asteroid.GetSpeed() * Time.deltaTime));
    }
    
    private void RandomAsteroidRain(float deltaTime)
    {
        if (_timeToSpawns <= 0)
        {
            // random pos around origin, speed and size
            SpawnAsteroid(
                spawnPositionManager.GetRandomPosition(),
                1f + Random.Range(-speedDeviation, speedDeviation),
                GetAsteroidBySize(GetRandomSize(chanceToSpawnLarger)));
            // random time till next spawn
            _timeToSpawns = Random.Range(minTimeToSpawn,maxTimeToSpawn);
        }
        else
        {
            _timeToSpawns -= deltaTime;
        }
    }

    private AsteroidSize GetRandomSize(float deviation, AsteroidSize size = AsteroidSize.Small)
    {
        while (true)
        {
            if (Random.Range(0f, 1f) < deviation && size != AsteroidSize.Large)
            {
                size = size + 1;
            }
            else
            {
                return size;
            }
        }
    }

    private Asteroid GetAsteroidBySize(AsteroidSize size)
    {
        return asteroidPrefabs.Find(asteroid => asteroid.GetSize() == size);
    }
    
    private void SpawnAsteroid(Vector2 position, float deviation, Asteroid target)
    {
        var asteroid = Instantiate(
            target.gameObject,
            position,
            Quaternion.identity,
            transform).GetComponent<Asteroid>();
        asteroid.DeviateSpeed(deviation);
        
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
