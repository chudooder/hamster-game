using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text text;

    [SerializeField] private Image image;
    
    [SerializeField] private float amount;
    
    private bool _started = false;

    private float _currentAmount = 0;
    

    public void StartTimer()
    {
        _started = true;
        _currentAmount = amount;
        GameManager.instance.ScoreListener += () =>
            _currentAmount -= GameManager.instance.CurrentScore;
    }

    private void EndTimer()
    {
        
    }
}
