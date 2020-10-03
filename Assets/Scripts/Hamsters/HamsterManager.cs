using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HamsterManager : MonoBehaviour
{
    private static HamsterManager instance;
    private static readonly int MinHamstersinGame = 3;
    private static readonly int MaxHamstersInGame = 6;
   
    private List<HamsterData> hamsters;
    [SerializeField] private HamsterDefaultValues hamsterDefaultValues;
    [SerializeField] private GameObject hamsterPrefab;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
 
        instance = this;
        Initialize();
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize()
    {
        hamsters = new List<HamsterData>();
        LoadHamsters();
        for (int i = hamsters.Count; i < MinHamstersinGame; ++i)
        {
            hamsters.Add(GenerateRandomHamster());
        }
    }

    private void LoadHamsters()
    {
        //todo
    }

    private HamsterData GenerateRandomHamster()
    {
        Color bellyColor = hamsterDefaultValues.bellyColors[Random.Range(0, hamsterDefaultValues.bellyColors.Count)];
        Color bodyColor = hamsterDefaultValues.bodyColors[Random.Range(0, hamsterDefaultValues.bodyColors.Count)];
        String firstName = hamsterDefaultValues.firstNames[Random.Range(0, hamsterDefaultValues.firstNames.Count)];
        String lastName = hamsterDefaultValues.lastNames[Random.Range(0, hamsterDefaultValues.lastNames.Count)];
        HamsterData hamster = new HamsterData(bodyColor, bellyColor, firstName,lastName);
        return hamster;
    }
    
    
}
