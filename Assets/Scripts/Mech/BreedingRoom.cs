using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingRoom : MonoBehaviour
{
    [SerializeField] private HamsterLocation _hamster1Location;
    [SerializeField] private HamsterLocation _hamster2Location;

    // Just a template
    void Breed()
    {
        //todo check if pair has already bred/assign some sort of cooldown or timer
        if (!_hamster1Location.Hamster || !_hamster2Location.Hamster || !HamsterManager.instance.CanBreed || !RunManager.Instance.CanHaveBabies) return;
        HamsterManager.instance.Breed(_hamster1Location.Hamster, _hamster2Location.Hamster);
    }
}
