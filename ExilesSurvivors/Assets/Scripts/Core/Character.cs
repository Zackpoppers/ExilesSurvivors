using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Statistic
{
    Life,
    Damage,
    Armor
}


public class StatsValue
{
    public Statistic statisticType;
    public int value;

    public StatsValue(Statistic statisticType, int value)
    {
        this.statisticType = statisticType;
        this.value = value;
    }
}

public class StatsGroup
{
    public List<StatsValue> stats;

    public StatsGroup()
    {
        stats = new List<StatsValue>();
        Init();
    }

    private void Init()
    {
        // Add stats in the order of the Statistic enum
        stats.Add(new StatsValue(Statistic.Life, 100)); // Life = index 0
        stats.Add(new StatsValue(Statistic.Damage, 25)); // Damage = index 1
        stats.Add(new StatsValue(Statistic.Armor, 5)); // Armor = index 2
    }

    public StatsValue Get(Statistic statisticToGet)
    {
        return stats[(int)statisticToGet];
    }
}

public enum Attribute
{
    Strength,
    Dexterity,
    Intelligence
}

[Serializable]
public class AttributeValue
{
    public Attribute attributeType;
    public int value;

    public AttributeValue(Attribute attributeType, int value = 0)
    {
        this.attributeType = attributeType;
        this.value = value;
    }
}

[Serializable]
public class AttributeGroup
{
    public List<AttributeValue> attributeValues;

    public AttributeGroup()
    {
        attributeValues = new List<AttributeValue>();
        Init();
    }

    public void Init()
    {
        attributeValues.Add(new AttributeValue(Attribute.Strength));
        attributeValues.Add(new AttributeValue(Attribute.Dexterity));
        attributeValues.Add(new AttributeValue(Attribute.Intelligence));
    }
}

[Serializable]
public class ValuePool
{
    public StatsValue maxValue;
    public int currentValue;

    public ValuePool(StatsValue maxValue)
    {
        this.maxValue = maxValue;
        this.currentValue = maxValue.value;
    }
}

public class Character : MonoBehaviour
{
    public StatsGroup stats;
    public ValuePool lifePool;

    private void Awake() // Changed from Start to Awake
    {
        // Initialize stats and life pool
        stats = new StatsGroup();
        lifePool = new ValuePool(stats.Get(Statistic.Life));

        // Debug log to confirm initialization
        Debug.Log($"LifePool Initialized: Max={lifePool.maxValue.value}, Current={lifePool.currentValue}");
    }

    public ValuePool LifePool => lifePool;

    public void TakeDamage(int damage)
    {
        damage = ApplyDamageMitigation(damage);
        lifePool.currentValue -= damage;

        Debug.Log($"{gameObject.name} took {damage} damage. Current Health: {lifePool.currentValue}");

        CheckDeath();
    }

    private int ApplyDamageMitigation(int damage)
    {
        damage -= stats.Get(Statistic.Armor).value;
        if (damage < 0)
        {
            damage = 1; // Ensure at least 1 damage is taken
        }
        return damage;
    }

    private void CheckDeath()
    {
        if (lifePool.currentValue <= 0)
        {
            Debug.Log($"{gameObject.name} has died!");
            HealthBarManager.Instance.RemoveHealthBar(this); // Remove health bar on death
            Destroy(gameObject);
        }
    }


    public StatsValue GetStat(Statistic statisticToGet)
    {
        return stats.Get(statisticToGet);
    }
}