using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HamsterStatus
{
    Unborn,
    Baby,
    Adult
}

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
    public HamsterStatus status;

    public HamsterData(Color bodyColor, Color bellyColor, string firstName, string lastName, HamsterStatus status) : 
        this(bodyColor, bellyColor, firstName, lastName, status, RandomStats()) {}
    
    public HamsterData(Color bodyColor, Color bellyColor, string firstName, string lastName, HamsterStatus status,
        Dictionary<Stats.StatType, int> statValues)
    {
        this.bodyColor = bodyColor;
        this.bellyColor = bellyColor;
        this.firstName = firstName;
        this.lastName = lastName;
        items = new Dictionary<Item.ItemSlot, Item>();
        statModifiers = new Dictionary<Stats.StatType, int>();
        foreach (Stats.StatType statType in Stats.AllStatTypes)
        {
            statModifiers.Add(statType, 1);
        }
        this.statValues = statValues;
        this.status = status;
    }
    
    private static Dictionary<Stats.StatType, int> RandomStats()
    {
        Dictionary<Stats.StatType, int> stats = new Dictionary<Stats.StatType, int>();
        foreach (Stats.StatType statType in Stats.AllStatTypes)
        {
            stats.Add(statType, RandomStat());
        }
        return stats;
    }

    private static int RandomStat()
    {
        int sum = 0;
        for (int i = Stats.StatMinimum; i <= Stats.StatMaximum; ++i)
        {
            sum += (Random.value < 0.6f) ? 1 : 0;
        }
        return sum;
    }

    private static int RandomSignedStatModifier()
    {
       int mod = (Random.value < 0.6f) ? 0 : 1;
       mod *= (Random.value < 0.6f) ? 1 : -1;
       return mod;
    }

    public static HamsterData Breed(HamsterData a, HamsterData b, string childFirstName)
    {
        System.Random rnd = new System.Random();
        Stats.StatType[] statTypes = Stats.AllStatTypes.OrderBy(x => rnd.Next()).ToArray();
        Dictionary<Stats.StatType, int> childStats = new Dictionary<Stats.StatType, int>();
        int split = statTypes.Length / 2;
        for (int i = 0; i < statTypes.Length; ++i)
        {
            Stats.StatType statType = statTypes[i];
            if (i < split)
            {
                childStats.Add(statType, a.statValues[statType] + RandomSignedStatModifier());
            }
            else
            {
                childStats.Add(statType, b.statValues[statType] + RandomSignedStatModifier());
            }
        }

        Color childBellyColor;
        Color childBodyColor;
        float colorSelector = Random.value;
        if (colorSelector < 0.5f)
        {
            childBellyColor = a.bellyColor + b.bellyColor;
            childBodyColor = a.bodyColor + b.bodyColor;
        } else if (colorSelector < 0.75f)
        {
            childBellyColor = a.bellyColor;
            childBodyColor = a.bodyColor;
        }
        else
        {
            childBellyColor = b.bellyColor;
            childBodyColor = b.bodyColor;
        }

        string lastName;
        float nameSelector = Random.value;
        if (nameSelector < 0.5f)
        {
            lastName = a.lastName;
        } else
        {
            lastName = a.firstName;
        }
        
        return new HamsterData(childBodyColor, childBellyColor, childFirstName, lastName, HamsterStatus.Unborn, childStats);
    }
}
