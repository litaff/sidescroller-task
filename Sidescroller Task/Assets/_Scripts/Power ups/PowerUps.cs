using System;
using TMPro;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private Shield shield;
    [SerializeField] private Blast blast;
    [SerializeField] private TMP_Text shieldChargeDisplay;
    [SerializeField] private TMP_Text blastChargeDisplay;

    public static Action<Asteroid> OnAsteroidCollision;

    private void Awake()
    {
        GameManager.OnLaunch += InitPowerUps;
        PowerUpCharge.OnChargePickUp += ChargePickUp;
    }

    private void InitPowerUps()
    {
        shield.Init();
        shieldChargeDisplay.text = shield.GetCharges().ToString();
        blast.Init();
        blastChargeDisplay.text = blast.GetCharges().ToString();
    }
    
    public void UpdatePowerUps()
    {
        shield.Progress(Time.deltaTime);
        shieldChargeDisplay.text = shield.GetCharges().ToString();
        blast.Progress(Time.deltaTime);
        blastChargeDisplay.text = blast.GetCharges().ToString();

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
