using System;
using System.Collections.Generic;

public class Stats
{
    public enum StatType
    {
        Speed,
        Acceleration,
        Motivation,
        Stamina
    }
    
    public static readonly Dictionary<StatType, string> StatDescriptions = new Dictionary<StatType, string>
    {
        { StatType.Speed, "Max velocity a hamster can go." }, 
        { StatType.Acceleration, "Effect that one tap has on increasing a hamster's velocity." },
        { StatType.Motivation, "How long a hamster maintain their speed after being tapped." },
        { StatType.Stamina, "How long the hamsters can run before getting tired."}
    };
    
    public static readonly int StatMinimum = 1;
    public static readonly int StatMaximum = 5;

    public static int NumStats
    {
        get { return AllStatTypes.Length; }
    }

    public static StatType[] AllStatTypes
    {
        get { return (Stats.StatType[]) Enum.GetValues(typeof(Stats.StatType)); }
    }
}

