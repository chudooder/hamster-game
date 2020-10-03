using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HammyRunSimple : HammyRun
{
    public float RotMaxFactor = 20;
    public float SpeedFactor = 2;

    private float _startRot;
    
    private void Start()
    {
        _startRot = transform.localRotation.eulerAngles.z;
        StartCoroutine(HammyRunRoutine());
    }

    public override void SetSpeed(float speed) {
        this.SpeedFactor = 0.02f * speed;
        this.RotMaxFactor = 1.5f * speed;
    }

    IEnumerator HammyRunRoutine()
    {
        Quaternion startRot = transform.localRotation;
        float t;
        Quaternion target, start;
        
        while (true)
        {
            t = 0;
            target = Quaternion.Euler(0,0, _startRot + RotMaxFactor);
            start = transform.localRotation;
            while (t <= 1)
            {
                transform.localRotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.localRotation = target;
            
            t = 0;
            target = Quaternion.Euler(0,0, _startRot);
            start = transform.localRotation;
            while (t <= 1)
            {
                transform.localRotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.localRotation = target;

            t = 0;
            target = Quaternion.Euler(0,0, _startRot - RotMaxFactor);
            start = transform.localRotation;
            while (t <= 1)
            {
                transform.localRotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.localRotation = target;
            
            t = 0;
            target = Quaternion.Euler(0,0, _startRot);
            start = transform.localRotation;
            while (t <= 1)
            {
                transform.localRotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.localRotation = target;
            yield return null;
        }
    }
}
