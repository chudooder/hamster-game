using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{

    public Rocket RocketPrefab;
    public float Speed = 2;

    
    [SerializeField] private Transform[] _rocketLoadLocations;
    [SerializeField] private float _noFireRadius = 5;
    public Transform Target;

    public float IdealDistanceFromTarget = 5f;
        
    private Rocket[] _rockets;


    private EnemyEyes _eyes;
    private EnemyRocketLauncher _rocketLauncher;

    private void Awake()
    {
        _eyes = GetComponentInChildren<EnemyEyes>();
        _rocketLauncher = GetComponentInChildren<EnemyRocketLauncher>();

        _eyes.FollowObject = Target;
        _rocketLauncher.FollowObject = Target;
    }

    private void Start()
    {
        _rockets = new Rocket[_rocketLoadLocations.Length];
        for (int i = 0; i < _rockets.Length; i++)
        {
            _rockets[i] = Instantiate(RocketPrefab, _rocketLoadLocations[i]);
        }

        StartCoroutine(LoadAndFire());
        StartCoroutine(MoveRoutine());
    }

    private void Update()
    {
        transform.rotation = Target.rotation;
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            var angle = ChooseDestinationAngle();
            
            yield return PathToPoint(angle);
            yield return Idle(4, angle);
            yield return null;
        }
    }

    private IEnumerator PathToPoint(float angle)
    {
        Vector2 dir = Quaternion.AngleAxis(angle, Vector3.forward) * (Vector3)Vector2.right ;
        var point = (Vector2)Target.transform.position + dir.normalized * IdealDistanceFromTarget;
        
        var move = (point - (Vector2)transform.position).normalized * Speed;
        while (Vector2.Distance(point, transform.position) > move.magnitude * Time.deltaTime)
        {
            transform.Translate(move * Time.deltaTime, Space.World);
            yield return null;
            point = (Vector2)Target.transform.position + dir.normalized * IdealDistanceFromTarget;
            move = (point - (Vector2)transform.position).normalized * Speed;
        }
        yield return null;
        transform.position = point;
    }
    
    private IEnumerator Idle(float time, float angle)
    {
        float t = 0;

        while (t < time)
        {
            Vector2 dir = Quaternion.AngleAxis(angle, Vector3.forward) * (Vector3)Vector2.right ;
            var point = (Vector2)Target.transform.position + dir.normalized * IdealDistanceFromTarget;
            var move = (point - (Vector2)transform.position).normalized * Speed;
            if (Vector2.Distance(transform.position, point) < move.magnitude)
                transform.position = point;
            else
                transform.Translate(move, Space.World);
            yield return null;
            t += Time.deltaTime;
        }
        
        
        yield return new WaitForSeconds(time);
    }

    private float ChooseDestinationAngle()
    {
        Random r = new Random();
        float angle = r.Next(180) - 90;
        float currAngle = Vector2.SignedAngle(Vector2.right, (transform.position - Target.transform.position).normalized);
        angle += currAngle;

        return angle;
     
    }

    
    
    IEnumerator LoadAndFire()
    {
        var overlapDelay = new WaitForSeconds(1);
        
        while (true)
        {
            for (int i = 0; i < _rockets.Length; i++)
            {
                while (Vector2.Distance(Target.position, transform.position) < _noFireRadius)
                {
                    yield return overlapDelay;
                }
                
                _rockets[i].Fire(Target);
                _rockets[i] = null;
                yield return new WaitForSeconds(.25f);
                
                _rockets[i] = Instantiate(RocketPrefab, _rocketLoadLocations[i]);
                yield return new WaitForSeconds(1);
            }
            
            yield return null;
        }
    }
    
}
