using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PowerUpChargeManager : MonoBehaviour
{
    [SerializeField] private List<PowerUpCharge> powerUpChargePrefabs;
    [SerializeField] private float speed;
    [SerializeField] private float chanceToSpawn;
    [SerializeField] private SpawnPositionManager spawnPositionManager;
    private bool _spawnPowerUps;
    private List<PowerUpCharge> _activeCharges;
    
    private void Awake()
    {
        _spawnPowerUps = false;
        _activeCharges = new List<PowerUpCharge>();
        GameManager.OnLaunch += ClearPowerUps;
        GameManager.OnPlay += StartSpawning;
        GameManager.OnLose += StopSpawning;
    }

    private void Update()
    {
        if (!_spawnPowerUps) return;
        
        RandomPowerUpSpawn(Time.deltaTime);
        foreach (var powerUpCharge in _activeCharges.Where(asteroid => asteroid))
        {
            MoveCharge(powerUpCharge);
        }
    }

    private void RandomPowerUpSpawn(float time)
    {
        if (Random.Range(0f, 100f) < chanceToSpawn * time)
        {
            SpawnPowerUp(spawnPositionManager.GetRandomPosition());
        }
    }
    
    private void SpawnPowerUp(Vector2 position)
    {
        var index = Random.Range(0, powerUpChargePrefabs.Count);
        var charge = Instantiate(
            powerUpChargePrefabs[index].gameObject, 
            position, 
            Quaternion.identity).GetComponent<PowerUpCharge>();
        _activeCharges.Add(charge);
    }

    private void MoveCharge(PowerUpCharge powerUpCharge)
    {
        powerUpCharge.transform.Translate(Vector2.down * (speed * Time.deltaTime));
    }

    private void StopSpawning()
    {
        _spawnPowerUps = false;
    }

    private void StartSpawning()
    {
        _spawnPowerUps = true;
    }

    private void ClearPowerUps()
    {
        while (_activeCharges.Count > 0)
        {
            var toDestroy = _activeCharges[0];
            _activeCharges.RemoveAt(0);
            DestroyCharge(toDestroy);
        }
    }
    
    private void DestroyCharge(PowerUpCharge charge)
    {
        _activeCharges.Remove(charge);
        Destroy(charge.gameObject);
    }
    
    // for kill zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Power up"))
        {
            DestroyCharge(other.GetComponent<PowerUpCharge>());
        }
    }
}
