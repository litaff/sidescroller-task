using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;
    
    private bool _scoring;
    private float _timeScore; // hidden score

    private int Score => Mathf.FloorToInt(_timeScore); // factual score counts as floored time survived
    
    private void Awake()
    {
        PowerUps.OnAsteroidCollision += AsteroidBonus;
        GameManager.OnLaunch += Init;
        GameManager.OnPlay += StartScoring;
        GameManager.OnLose += StopScoring;
    }

    private void Init()
    {
        _timeScore = 0;
        _scoring = false;
        scoreDisplay.enabled = false;
    }

    private void Update()
    {
        DisplayScore(); // always display 
        
        if (!_scoring) return;
        
        IncreaseScore(Time.deltaTime);
    }

    private void DisplayScore()
    {
        scoreDisplay.text = Score.ToString();
    }
    
    private void AsteroidBonus(Asteroid asteroid)
    {
        _timeScore += asteroid.GetBonus();
    }
    
    private void IncreaseScore(float time)
    {
        _timeScore += time;
    }

    private void StartScoring()
    {
        _scoring = true;
        scoreDisplay.enabled = true;
    }

    private void StopScoring()
    {
        _scoring = false;
    }
}
