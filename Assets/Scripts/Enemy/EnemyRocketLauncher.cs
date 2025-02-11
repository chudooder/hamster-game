﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketLauncher : MonoBehaviour
{
    public Transform FollowObject;
    [SerializeField] private float _rotSpeed = 5; 

    private void Update()
    {
        Vector2 dir = (FollowObject.position - transform.position).normalized;
        Quaternion target = Quaternion.FromToRotation(Vector2.right, dir);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _rotSpeed);
    }
}
