using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SupportGem : ScriptableObject
{
    [Header("Base Support Gem Properties")]
    public string gemName;
    public string description;
    public Sprite icon;

    // Method to modify a skill gem
    public virtual void ApplySupport(SkillGem skill)
    {
        skill.OnProjectileSpawned += HandleProjectileSpawned;
    }

    protected virtual void HandleProjectileSpawned(Projectile projectile)
    {
        
    }
}
