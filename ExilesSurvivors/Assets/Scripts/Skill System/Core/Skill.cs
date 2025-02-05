using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class SkillGem : ScriptableObject
{
    [Header("Base Settings")]
    public GameObject projectilePrefab;
    public string Name;
    public float Cooldown = 1f;
    public float BaseDamage = 10f;
    public List<SupportGem> SupportGems = new List<SupportGem>();
    public event Action<Projectile> OnProjectileSpawned;
    public event Action<Player, Projectile> OnSkillActivate;

    private float lastUsedTime;

    public virtual void Activate(Player player)
    {
        
        //if (Time.time - lastUsedTime < Cooldown) return;
        

        Vector2 direction = GetShootingDirection(player);
        SpawnProjectile(player.transform.position, direction);
        lastUsedTime = Time.time;
    }

    public Vector2 GetShootingDirection(Player player)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePos - player.transform.position).normalized;
    }

    protected virtual void SpawnProjectile(Vector2 position, Vector2 direction)
    {
        var projectileObj = Instantiate(projectilePrefab, position, Quaternion.identity);
        if (projectileObj.TryGetComponent<Projectile>(out var projectile))
        {
           
            projectile.Initialize(direction, projectile.Speed, BaseDamage);
             OnProjectileSpawned?.Invoke(projectile);
        }
    }
    protected void InvokeProjectileSpawned(Projectile projectile)
    {
        OnProjectileSpawned?.Invoke(projectile);
    }

    protected virtual float GetProjectileSpeed() => 10f;

    protected virtual void OnEnable()
    {
        // Reset event when entering play mode
        OnProjectileSpawned = null;
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        #endif
    }

    #if UNITY_EDITOR
    private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            OnProjectileSpawned = null;
        }
    }
    #endif
}