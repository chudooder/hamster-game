using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _spawnRate = 5;
    [SerializeField] private float _baseSpawnWait = 5;
    [SerializeField] private float _spawnRadius = 30;
    public Transform Target;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float t, wait;
            t = 0;
            do
            {
                wait = _baseSpawnWait  / ((GameManager.instance.CurrentScore + 1) * _spawnRate);
                t += Time.deltaTime;
                yield return null;
            } while (t < wait);

            Instantiate(_enemy, CalculateEnemySpawnLocation(), Quaternion.identity).Target = Target;
            yield return null;
        }
    }

    Vector2 CalculateEnemySpawnLocation()
    {
        Random r = new Random();
        float angle = r.Next(360);
        float currAngle = Vector2.SignedAngle(Vector2.right, (transform.position - Target.transform.position).normalized);
        angle += currAngle;
        Vector2 dir = Quaternion.AngleAxis(angle, Vector3.forward) * (Vector3)Vector2.right;
        var point = (Vector2)Target.transform.position + dir.normalized * _spawnRadius;
        
        return point;
    }
}
