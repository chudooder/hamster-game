using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float RocketLifetime = 10;
    public float RotationSpeed = 3;
    public float RocketMoveSpeed = 3;

    private Transform _target;
    
    private IEnumerator FireRoutine(Transform target)
    {
        _target = target;
        
        float t = 0;
        while (t <= RocketLifetime)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            Quaternion targetRot = Quaternion.FromToRotation(Vector2.right, dir);
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * RotationSpeed);
            transform.position += RocketMoveSpeed * Time.deltaTime * transform.TransformDirection(Vector2.right);
            t += Time.deltaTime;
        }
    }

    public virtual void OnHit()
    {
        // TODO: deal damage
        Destroy(gameObject);
    }

    public virtual void Fire(Transform target)
    {
        StartCoroutine(FireRoutine(target));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _target)
        {
            OnHit();
        }
    }
}
