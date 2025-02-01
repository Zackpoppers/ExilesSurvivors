using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string Name;
    public float Cooldown = 2f;
    public int Level = 1;
    public bool IsAwakened = false;

    private float _lastUsedTime;

    public void Activate()
    {
        if (Time.time - _lastUsedTime < Cooldown) return;
        Debug.Log($"{Name} activated!");
        _lastUsedTime = Time.time;
    }

    public void LevelUp()
    {
        Level++;
        Debug.Log($"{Name} leveled up to {Level}!");
    }
}