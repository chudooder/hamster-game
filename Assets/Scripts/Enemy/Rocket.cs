using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float RocketLifetime = 10;
    public float RotationSpeed = 3;
    public float RocketMoveSpeed = 3;

    [SerializeField] private GameObject _thrust;
    
    private Transform _target;

    private void Start()
    {
        _thrust.SetActive(false);
    }


    private IEnumerator FireRoutine(Transform target)
    {
        _target = target;
        _thrust.SetActive(true);

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

        OnTimeout();
    }

    public virtual void OnHit()
    {
        // TODO: deal damage
        Destroy(gameObject);
    }

    public virtual void OnTimeout()
    {
        Destroy(gameObject);
    }
    
    public virtual void Fire(Transform target)
    {
        transform.parent = null;
        StartCoroutine(FireRoutine(target));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _target)
        {
            OnHit();
        }
    }
}
