using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HamsterDefaults", menuName = "ScriptableObjects/Hamsters", order = 0)]
public class HamsterDefaultValues : ScriptableObject
{
    public List<Color> bodyColors;
    public List<Color> bellyColors;
    public List<string> firstNames;
    public List<string> lastNames;
}
