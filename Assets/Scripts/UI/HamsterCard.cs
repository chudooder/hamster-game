using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterCard : MonoBehaviour
{
    private static Hamster _currentHamster;
    public static Hamster CurrentHamster
    {
        get { return _currentHamster; }
        set
        {
            _currentHamster = value;
            if (!_currentHamster)
                OnHide();
            else
                OnChange();
        }
    }

    public static Action OnHide;
    public static Action OnChange;

    [SerializeField] private Image _HamsterBase;    
    [SerializeField] private Image _HamsterBelly;
    [SerializeField] private Text _acceleration;
    [SerializeField] private Text _speed;
    [SerializeField] private Text _motivation;
    [SerializeField] private Text _stamina;
    [SerializeField] private Text _name;

    public void Start()
    {
        OnHide += HideCard;
        OnChange += ChangeCard;
        CurrentHamster = null;
    }

    private void OnDestroy()
    {
        OnHide -= HideCard;
        OnChange -= OnChange;
    }

    public void HideCard()
    {
        gameObject.SetActive(false);
        
    }

    public void ChangeCard()
    {
        gameObject.SetActive(true);

        _HamsterBase.color = CurrentHamster.HamsterData.bodyColor;
        _HamsterBelly.color = CurrentHamster.HamsterData.bellyColor;
        _acceleration.text = CurrentHamster.GetStat(Stats.StatType.Acceleration).ToString();
        _speed.text = CurrentHamster.GetStat(Stats.StatType.Speed).ToString();
        _motivation.text = CurrentHamster.GetStat(Stats.StatType.Motivation).ToString();
        _stamina.text = CurrentHamster.GetStat(Stats.StatType.Stamina).ToString();

        _name.text = CurrentHamster.HamsterData.firstName + " " + CurrentHamster.HamsterData.lastName;
    }
}
