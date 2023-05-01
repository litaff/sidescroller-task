using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TMP_Text clickToPlay;
    [SerializeField] private TMP_Text clickToPlayAgain;
    [SerializeField] private GameObject shieldTutorial;
    [SerializeField] private GameObject blastTutorial;
    [SerializeField] private GameObject moveTutorial;
    [SerializeField] private GameObject deathTutorial;

    private void Awake()
    {
        GameManager.OnLaunch += CloseClickToPlayAgain;
        GameManager.OnLaunch += DisplayClickToPlay;
        GameManager.OnLaunching += ClearTutorial;
        GameManager.OnLaunching += CloseClickToPlay;
        GameManager.OnLose += DisplayClickToPlayAgain;
        
        DisplayShieldTutorial();
        DisplayBlastTutorial();
        DisplayMoveTutorial();
        DisplayDeathTutorial();
    }

    private void CloseClickToPlay()
    {
        clickToPlay.enabled = false;
    }
    
    private void DisplayClickToPlay()
    {
        clickToPlay.enabled = true;
    }
    
    private void CloseClickToPlayAgain()
    {
        clickToPlayAgain.enabled = false;
    }
    
    private void DisplayClickToPlayAgain()
    {
        clickToPlayAgain.enabled = true;
    }

    private void DisplayShieldTutorial()
    {
        shieldTutorial.SetActive(true);
    }

    private void DisplayBlastTutorial()
    {
        blastTutorial.SetActive(true);
    }

    private void DisplayMoveTutorial()
    {
        moveTutorial.SetActive(true);
    }

    private void DisplayDeathTutorial()
    {
        deathTutorial.SetActive(true);
    }

    private void ClearTutorial()
    {
        shieldTutorial.SetActive(false);
        blastTutorial.SetActive(false);
        moveTutorial.SetActive(false);
        deathTutorial.SetActive(false);
    }
}
