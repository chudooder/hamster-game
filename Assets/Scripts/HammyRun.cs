using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HammyRun : MonoBehaviour
{
    public float RotMaxFactor = 20;
    public float SpeedFactor = 2;
    
    private float _startRot;
    
    private void Start()
    {
        _startRot = transform.rotation.eulerAngles.z;
        StartCoroutine(HammyRunRoutine());
    }

    IEnumerator HammyRunRoutine()
    {
        Quaternion startRot = transform.rotation;
        float t;
        Quaternion target, start;
        
        while (true)
        {
            t = 0;
            target = Quaternion.Euler(0,0, _startRot + RotMaxFactor);
            start = transform.rotation;
            while (t <= 1)
            {
                transform.rotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.rotation = target;
            
            t = 0;
            target = Quaternion.Euler(0,0, _startRot);
            start = transform.rotation;
            while (t <= 1)
            {
                transform.rotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.rotation = target;

            t = 0;
            target = Quaternion.Euler(0,0, _startRot - RotMaxFactor);
            start = transform.rotation;
            while (t <= 1)
            {
                transform.rotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.rotation = target;
            
            t = 0;
            target = Quaternion.Euler(0,0, _startRot);
            start = transform.rotation;
            while (t <= 1)
            {
                transform.rotation = Quaternion.Slerp(start, target, t);
                yield return null;
                t += Time.deltaTime * RotMaxFactor * SpeedFactor;
            }
            transform.rotation = target;
            yield return null;
        }
    }
}
