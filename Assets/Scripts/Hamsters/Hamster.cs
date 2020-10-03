using System.Collections.Generic;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer belly;
    private HamsterData _hamsterData;
    
    public void Initialize(HamsterData hamsterData)
    {
        _hamsterData = hamsterData;
        body.color = _hamsterData.bodyColor;
        belly.color = _hamsterData.bellyColor;
    }

}
