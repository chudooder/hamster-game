using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float _value = 1;
    private float _width;

    private RectTransform _rectTransform => (RectTransform) transform;

    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            var copy = _rectTransform.sizeDelta;
            copy.x = (_value -1) * _width;
            _rectTransform.sizeDelta = copy;
        }
    }

    private void Update()
    {
        var copy = _rectTransform.sizeDelta;
        copy.x = (_value -1) * _width;
        _rectTransform.sizeDelta = copy;
    }

    private void Start()
    {
        _width = _rectTransform.rect.width  /  _rectTransform.localScale.x;

        GameManager.instance.HealthListener +=
            () => Value = (float) GameManager.instance.CurrentHealth / GameManager.instance.MaxHealth;
    }
}
