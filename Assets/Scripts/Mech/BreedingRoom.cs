using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingRoom : MonoBehaviour
{
    [SerializeField] private HamsterLocation _hamster1Location;
    [SerializeField] private HamsterLocation _hamster2Location;
    [SerializeField] private int ScoreToBreed = 2000;
    private float startBreedScore = 0;
    private Coroutine _breedRoutine;


    public void SetHamster()
    {
        if (_hamster1Location.Hamster && _hamster2Location.Hamster)
        {
            Breed();
        }
    }

    
    
    public void Conceive()
    {
        HamsterManager.instance.Breed(_hamster1Location.Hamster, _hamster2Location.Hamster);
    }
    
    // Just a template
    void Breed()
    {
        //todo check if pair has already bred/assign some sort of cooldown or timer
        if (!_hamster1Location.Hamster || !_hamster2Location.Hamster || !HamsterManager.instance.CanBreed || !RunManager.Instance.CanHaveBabies) return;
        if (_breedRoutine != null) StopCoroutine(_breedRoutine);
        _breedRoutine = StartCoroutine(BreedRoutine(_hamster1Location.Hamster ,_hamster2Location.Hamster));
    }

    private IEnumerator BreedRoutine(Hamster h1, Hamster h2)
    {
        float startScore = GameManager.instance.CurrentScore;

        while (GameManager.instance.CurrentScore < startScore + ScoreToBreed)
        {
            if (!h1 || !h2 || h1 != _hamster1Location.Hamster ||
                h2 != _hamster2Location.Hamster)
            {
                yield break;
            }
            
            //TODO: Tick timer
            
            yield return null;
        }

        Conceive();
    }
}
