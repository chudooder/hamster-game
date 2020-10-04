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
    public int Health = 4;
    public float FireRate = 5;
    public float IdleTime = 8;
    
    [SerializeField] private Transform[] _rocketLoadLocations;
    [SerializeField] private float _noFireRadius = 5;
    [SerializeField] private Transform _target;

    public Transform Target
    {
        set
        {
            _target = value;
            Eyes.FollowObject = _target;
            RocketLauncher.FollowObject = _target;
        }
        get { return _target; }
    }

    public float IdealDistanceFromTarget = 5f;
        
    private Rocket[] _rockets;

    private EnemyEyes _eyes;
    public EnemyEyes Eyes => (_eyes != null)? _eyes : _eyes = GetComponentInChildren<EnemyEyes>(); 
    private EnemyRocketLauncher _rocketLauncher;
    public EnemyRocketLauncher RocketLauncher => (_rocketLauncher != null)? _rocketLauncher : _rocketLauncher = GetComponentInChildren<EnemyRocketLauncher>(); 
    
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
            yield return Idle(IdleTime, angle);
            yield return null;
        }
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0) Delete();
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private IEnumerator PathToPoint(float angle)
    {
        Vector2 dir = Quaternion.AngleAxis(angle + _target.rotation.eulerAngles.z, Vector3.forward) * (Vector3)Vector2.right ;
        var point = (Vector2)Target.transform.position + dir.normalized * IdealDistanceFromTarget;
        
        var move = (point - (Vector2)transform.position).normalized * Speed;
        while (Vector2.Distance(point, transform.position) > move.magnitude * Time.deltaTime)
        {
            transform.Translate(move * Time.deltaTime, Space.World);
            yield return null;
            dir = Quaternion.AngleAxis(angle + _target.rotation.eulerAngles.z, Vector3.forward) * (Vector3)Vector2.right ;
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
            Vector2 dir = Quaternion.AngleAxis(angle + _target.rotation.eulerAngles.z, Vector3.forward) * (Vector3)Vector2.right ;
            var point = (Vector2)Target.transform.position + dir.normalized * IdealDistanceFromTarget;
            var move = (point - (Vector2)transform.position).normalized * Speed;
            if (Vector2.Distance(transform.position, point) < move.magnitude)
                transform.position = point;
            else
                transform.Translate(move, Space.World);
            yield return null;
            t += Time.deltaTime;
        }
    }

    private float ChooseDestinationAngle()
    {
        Random r = new Random();
        float angle = r.Next(180);
        // float currAngle = Vector2.SignedAngle(Vector2.right, (transform.position - Target.transform.position).normalized);
        // angle += currAngle;

        return angle;
     
    }

    
    
    IEnumerator LoadAndFire()
    {
        var overlapDelay = new WaitForSeconds(1);
        float laodtime = .25f;
        while (true)
        {
            for (int i = 0; i < _rockets.Length; i++)
            {
                while (Vector2.Distance(Target.position, transform.position) < _noFireRadius)
                    yield return overlapDelay;
                
                Vector2 vpSpace = Camera.main.WorldToViewportPoint(transform.position);
                while (vpSpace.x < 0 || vpSpace.x > 1 || vpSpace.y < 0 || vpSpace.y > 1)
                {
                    yield return overlapDelay;
                    vpSpace = Camera.main.WorldToViewportPoint(transform.position);
                }
                    
                _rockets[i].Fire(Target);
                _rockets[i] = null;
                yield return new WaitForSeconds(laodtime);
                
                _rockets[i] = Instantiate(RocketPrefab, _rocketLoadLocations[i]);
                yield return new WaitForSeconds(FireRate - laodtime);
            }
            
            yield return null;
        }
    }
    
}
