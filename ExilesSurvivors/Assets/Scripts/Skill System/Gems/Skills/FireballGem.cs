// Modified FireballGem.cs
using UnityEngine;

[CreateAssetMenu(fileName = "FireballGem", menuName = "Gems/SkillGem/Fireball")]
public class FireballGem : SkillGem
{
    [Header("Fireball Settings")]
    public float projectileSpeed = 15f;
    public float explosionRadius = 2f;

    protected override void SpawnProjectile(Vector2 position, Vector2 direction)
    {
        // Remove base call and handle fireball-specific creation
        var projectileObj = Instantiate(projectilePrefab, position, Quaternion.identity);
        var fireball = projectileObj.GetComponent<FireballProjectile>();

        if (fireball != null)
        {
            fireball.Initialize(direction, projectileSpeed, BaseDamage);
            fireball.ExplosionRadius = explosionRadius;
            InvokeProjectileSpawned(fireball);
        }
    }

    protected override float GetProjectileSpeed() => projectileSpeed;
}