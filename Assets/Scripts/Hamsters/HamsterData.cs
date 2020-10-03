using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HamsterData
{
    public string firstName;
    public string lastName; 
    public Color bodyColor;
    public Color bellyColor;
    public Dictionary<Stats.StatType, int> statValues;
    public Dictionary<Stats.StatType, int> statModifiers;
    public Dictionary<Item.ItemSlot, Item> items;

    public HamsterData(Color bodyColor, Color bellyColor, string firstName, string lastName)
    {
        this.bodyColor = bodyColor;
        this.bellyColor = bellyColor;
        this.firstName = firstName;
        this.lastName = lastName;
        items = new Dictionary<Item.ItemSlot, Item>();
        RandomizeStats();
    }
    
    private void RandomizeStats()
    {
        statModifiers = new Dictionary<Stats.StatType, int>();
        statValues = new Dictionary<Stats.StatType, int>();
        foreach (Stats.StatType statType in Stats.AllStatTypes)
        {
            statModifiers.Add(statType, 0);
            statValues.Add(statType, RandomStat());
        } 
    }

    private int RandomStat()
    {
        int sum = 0;
        for (int i = Stats.StatMinimum; i <= Stats.StatMaximum; ++i)
        {
            sum += (Random.value < 0.6f) ? 1 : 0;
        }
        return sum;
    }
}
