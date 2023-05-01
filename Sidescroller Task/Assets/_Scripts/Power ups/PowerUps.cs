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
        PowerUpCharge.OnChargePickUp += ChargePickUp;
    }

    public void UpdatePowerUps()
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

    private void ChargePickUp(ChargeType type)
    {
        switch (type)
        {
            case ChargeType.Shield:
                shield.GainCharge();
                break;
            case ChargeType.Blast:
                blast.GainCharge();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
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
