using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("Base Gem Properties")]
    public string Name;
    public string Description;
    public Sprite Icon;
    public float Cooldown = 1f;
    public float BaseDamage = 1f;
    public int Level = 1;
    public bool IsAwakened = false;

    private float _lastUsedTime;

    public virtual void Activate(Player player)
    {
        if (Time.time - _lastUsedTime < Cooldown) return;
        Debug.Log($"{Name} activated!");
        _lastUsedTime = Time.time;
    }

    public virtual void LevelUp()
    {
        Level++;
        Debug.Log($"{Name} leveled up to {Level}!");
    }
}