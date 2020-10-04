using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private static RunManager _instance = null;
    public static RunManager Instance => (_instance) ? _instance : _instance = FindObjectOfType<RunManager>();
    
    public List<HamsterLocation> HamsterLocations = new List<HamsterLocation>();
}
