using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedLocation : HamsterLocation
{
    [SerializeField] private BreedingRoom _room;

    public override  Hamster Hamster
    {
        get
        {
            return _hamster;
        }
        set
        {
            _hamster = value;

            if (_hamster != null)
            {
                _hamster.transform.parent = _hamsterSpawnLocation;
                _hamster.transform.position = _hamsterSpawnLocation.position;
                _hamster.transform.rotation = _hamsterSpawnLocation.rotation;
            }

            _room.SetHamster();
        }
    }
}
