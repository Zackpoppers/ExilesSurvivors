using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier // Class name must be "Modifier"
{
    public string Name;
    public string Effect;
    public string Description;

    public void ApplyEffect(Enemy enemy)
    {
        Debug.Log($"Applying {Name} to enemy!");
    }
}