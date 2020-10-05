using System.Collections.Generic;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private List<SpriteRenderer> belly;
    [SerializeField] private Timer babyTimer;
    [SerializeField] private Timer adultTimer;
    [SerializeField] private GameObject adultBody;
    [SerializeField] private float babyScale = 0.5f;

    private HamsterData _hamsterData;
    public HamsterData HamsterData => _hamsterData;
    
    public void Initialize(HamsterData hamsterData)
    {
        _hamsterData = hamsterData;
        body.color = _hamsterData.bodyColor;
        foreach (SpriteRenderer sprite in belly)
        {
            sprite.color = _hamsterData.bellyColor;
        }
    
        if (_hamsterData.status == HamsterStatus.Unborn)
        {
            Debug.Log("still unborn");
            InitializeBaby();
        }
    }

    public int GetStat(Stats.StatType statType)
    {
        return _hamsterData.statModifiers[statType] + _hamsterData.statValues[statType];
    }

    public HamsterData Breed(Hamster hamster)
    {
        return HamsterData.Breed(hamster._hamsterData, _hamsterData, HamsterManager.instance.GenerateRandomFirstName());
    }

    public void Mature()
    {
        bool changed = _hamsterData.Mature();
        if (changed)
        {
            if (_hamsterData.status == HamsterStatus.Baby)
            {
                babyTimer.gameObject.SetActive(false);
                adultTimer.gameObject.SetActive(true);
                adultTimer.StartTimer(HamsterManager.instance.ScoreNeededToMature);
                adultBody.SetActive(true);
                adultBody.transform.localScale = Vector3.one * babyScale;
            } else if (_hamsterData.status == HamsterStatus.Adult)
            {
                adultTimer.gameObject.SetActive(false);
                adultBody.transform.localScale = Vector3.one;
            }
        }
    }

    private void InitializeBaby()
    {
        Debug.Log("initializing bb");
        babyTimer.gameObject.SetActive(true);
        adultBody.gameObject.SetActive(false);
        babyTimer.StartTimer(HamsterManager.instance.ScoreNeededToBeBorn);
    }
    
}
