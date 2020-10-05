using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    
    [SerializeField] private Hamster hamster;
    
    private bool _started = false;
    private float _amount;

    private float _currentAmount = 0;

    public void StartTimer(float amount)
    {
        _started = true;
        _currentAmount = amount;
        _amount = amount;
        GameManager.instance.ScoreListener += UpdateTimer;
    }

    private void UpdateTimer()
    {
        if (!_started) return;
        _currentAmount -= GameManager.instance.CurrentScore;
        timerImage.fillAmount = _currentAmount / _amount;
        if (_currentAmount <= 0)
        {
            EndTimer();
        }
    }

    private void EndTimer()
    {
        GameManager.instance.ScoreListener -= UpdateTimer;
        _started = false;
        hamster.Mature();
    }
}
