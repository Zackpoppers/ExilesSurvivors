using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Name; // Add this line
    public string Description;
    public Dictionary<string, float> Stats;

    public void Equip(Player player)
    {
        Debug.Log($"Equipped {Name}!");
    }

    public void Unequip(Player player)
    {
        Debug.Log($"Unequipped {Name}!");
    }
}