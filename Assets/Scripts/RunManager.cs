using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private static RunManager _instance = null;
    public static RunManager Instance => (_instance) ? _instance : _instance = FindObjectOfType<RunManager>();
    
    public List<HamsterLocation> HamsterLocations = new List<HamsterLocation>();
    public List<HamsterLocation> BabyLocations = new List<HamsterLocation>();

    public bool CanHaveBabies => CountBabies() > 0;

    private int CountBabies()
    {
        int i = 0;
        foreach (HamsterLocation location in BabyLocations)
        {
            if (location.Hamster == null)
            {
                i++;
            }
        }
        return i;
    }

    
    public List<HamsterLocation> PlacableHamsterLocations = new List<HamsterLocation>();

    private void Awake()
    {
        HamsterManager.instance.BeginRun();
    }
}
