using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Rocket RocketPrefab;

    [SerializeField] private Transform[] _rocketLoadLocations;


    IEnumerator LoadAndFire()
    {
        while (true)
        {
            foreach (Transform location in _rocketLoadLocations)
            {
                
            }
            
            yield return null;
        }
    }
    
}
