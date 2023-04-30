using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private Shield shield;

    
    [SerializeField] private Blast blast;

    public static Action<Asteroid> OnAsteroidCollision;

    private void Awake()
    {
        shield.Init();
        blast.Init();
    }

    private void Update()
    {
        shield.Progress(Time.deltaTime);
        blast.Progress(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (shield.IsActive)
            {
                shield.End();
            }
            else
            {
                shield.OnUse();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (blast.IsActive)
            {
                blast.End();
            }
            else
            {
                blast.OnUse();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shield.IsActive || blast.IsActive)
        {
            if (other.CompareTag("Asteroid"))
            {
                OnAsteroidCollision?.Invoke(other.GetComponent<Asteroid>());
            }
        }
    }
}
