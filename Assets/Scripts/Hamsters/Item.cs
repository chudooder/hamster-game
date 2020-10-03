using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Hamsters", order = 1)]
public class Item : ScriptableObject
{
    public enum ItemSlot
    {
        Head,
        Body,
        Feet
    }

    public static readonly int StatImprovementValue = 1;

    public ItemSlot slot;
    public string itemName;
    public Stats.StatType improvedStat;
    public Texture2D itemImage;

}
