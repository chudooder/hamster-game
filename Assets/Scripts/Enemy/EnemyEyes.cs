using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyes : MonoBehaviour
{
    [SerializeField] private Transform _followObject;
    [SerializeField] private float _followRadius;
    private Vector2 _startPos;
    
    void Start()
    {
        _startPos = transform.localPosition;
    }

    void Update()
    {
        Vector2 dir = ((Vector2)_followObject.position - (Vector2)transform.TransformPoint(_startPos)).normalized;
        transform.localPosition = _startPos + (Vector2)transform.InverseTransformVector(dir).normalized * _followRadius;
        
    }
}
