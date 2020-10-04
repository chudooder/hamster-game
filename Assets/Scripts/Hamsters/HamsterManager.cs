using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HamsterManager : MonoBehaviour
{
    public static HamsterManager instance;
    private static int MinHamstersinGame = 4;
    private static int MaxHamstersInGame = 6;
   
    private List<HamsterData> hamsters;
    private List<HamsterData> chosenHamsters;
    private List<GameObject> currentHamsters;
    private HammyRunPhysics[] wheelPositions;
    [SerializeField] private HamsterDefaultValues hamsterDefaultValues;
    [SerializeField] private GameObject hamsterPrefab;
    [SerializeField] private int ScoreNeededToMature;
    [SerializeField] private int ScoreNeededToBeBorn;

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
        for (int i = hamsters.Count; i < MinHamstersinGame; ++i)
        {
            hamsters.Add(GenerateRandomHamster());
        }

    }
    
    public void BeginRun()
    {
        currentHamsters = new List<GameObject>();
        chosenHamsters = hamsters; //todo loadouts
        wheelPositions = FindObjectsOfType<HammyRunPhysics>();
        Debug.Log(wheelPositions.Length);
        int i = 0;
        foreach (HamsterData hamsterData in chosenHamsters)
        {
            GameObject hamObj = Instantiate(hamsterPrefab, wheelPositions[i].transform, false);
            hamObj.GetComponent<Hamster>().Initialize(hamsterData);
            //todo put in hamster in right pos with right parent
            i++;
        }
    }

    private HamsterData GenerateRandomHamster()
    {
        Color bellyColor = hamsterDefaultValues.bellyColors[Random.Range(0, hamsterDefaultValues.bellyColors.Count)];
        Color bodyColor = hamsterDefaultValues.bodyColors[Random.Range(0, hamsterDefaultValues.bodyColors.Count)];
        String firstName = hamsterDefaultValues.firstNames[Random.Range(0, hamsterDefaultValues.firstNames.Count)];
        String lastName = hamsterDefaultValues.lastNames[Random.Range(0, hamsterDefaultValues.lastNames.Count)];
        HamsterData hamster = new HamsterData(bodyColor, bellyColor, firstName,lastName, HamsterStatus.Adult);
        return hamster;
    }
    
    
}
