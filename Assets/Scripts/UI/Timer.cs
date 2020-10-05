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
    private float _target;
    
    public void StartTimer(float target)
    {
        _started = true;
        _target = target + GameManager.instance.CurrentScore;
        GameManager.instance.ScoreListener += UpdateTimer;
    }

    private void UpdateTimer()
    {
        if (!_started) return;
        timerImage.fillAmount = GameManager.instance.CurrentScore/ _target;
        if (GameManager.instance.CurrentScore >= _target)
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
