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
    private List<Hamster> currentHamsters;
    [SerializeField] private HamsterDefaultValues hamsterDefaultValues;
    [SerializeField] private GameObject hamsterPrefab;
    [SerializeField] public int ScoreNeededToMature;
    [SerializeField] public int ScoreNeededToBeBorn;

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
        currentHamsters = new List<Hamster>();
        chosenHamsters = hamsters; //todo loadouts
        int i = 0;
        foreach (HamsterData hamsterData in chosenHamsters)
        {
            Hamster hamObj = Instantiate(hamsterPrefab, null, false).GetComponent<Hamster>();
            hamObj.Initialize(hamsterData);
            currentHamsters.Add(hamObj);
            RunManager.Instance.HamsterLocations[i].Hamster = hamObj;
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

    public string GenerateRandomFirstName()
    {
        string firstName = hamsterDefaultValues.firstNames[Random.Range(0, hamsterDefaultValues.firstNames.Count)];
        return firstName;
    }

    public bool CanBreed => MaxHamstersInGame > currentHamsters.Count;

    public void Breed(Hamster hamster, Hamster hamster1)
    {
        HamsterData child = hamster.Breed(hamster1);
        chosenHamsters.Add(child);
        Debug.Log("Child bred. Status is:  " + child.status);
        Hamster childObj = Instantiate(hamsterPrefab, null, false).GetComponent<Hamster>();
        childObj.Initialize(child);
        currentHamsters.Add(childObj);
        foreach (HamsterLocation location in RunManager.Instance.BabyLocations)
        {
            if (location.Hamster == null)
            {
                location.Hamster = childObj;
                break;
            }
        }
        //todo make these permanent/between runs
    }

}
