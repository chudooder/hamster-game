using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingRoom : MonoBehaviour
{
    [SerializeField] private HamsterLocation _hamster1Location;
    [SerializeField] private HamsterLocation _hamster2Location;
    [SerializeField] private int ScoreToBreed = 20000;
    private Hamster _breed1;
    private Hamster _breed2;
    private float startBreedScore = 0;
    

    private void Update()
    {
        if (_breed1 && _breed2 && _breed1 == _hamster1Location.Hamster &&
            _breed2 == _hamster2Location.Hamster)
        {
            //todo tick the clock
        }
        else
        {
            
        }
    }

    // Just a template
    void Breed()
    {
        //todo check if pair has already bred/assign some sort of cooldown or timer
        if (!_hamster1Location.Hamster || !_hamster2Location.Hamster || !HamsterManager.instance.CanBreed || !RunManager.Instance.CanHaveBabies) return;
        HamsterManager.instance.Breed(_hamster1Location.Hamster, _hamster2Location.Hamster);
    }
}
