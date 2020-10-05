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
        if (!_hamster1Location.Hamster || !_hamster2Location.Hamster || !HamsterManager.instance.CanBreed) return;
        HamsterManager.instance.Breed(_hamster1Location.Hamster, _hamster2Location.Hamster);
    }
}
