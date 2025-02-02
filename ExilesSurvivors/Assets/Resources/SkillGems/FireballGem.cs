using UnityEngine;

[CreateAssetMenu(fileName = "FireballGem", menuName = "Gems/SkillGem/Fireball")]
public class FireballGem : Skill
{
    [Header("Fireball Settings")]
    public float projectileSpeed = 10f;
    public float explosionRadius = 1f;

    // Event triggered when a projectile is spawned
    public System.Action<FireballProjectile> OnProjectileSpawned;

    public override void Activate(Player player)
    {
        if (projectilePrefab == null) return;

        //Shooting at cursor
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector2 direction = (mouseWorldPos - player.transform.position).normalized;


        GameObject fireball = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        FireballProjectile projectile = fireball.GetComponent<FireballProjectile>();

        if (projectile != null)
        {
            projectile.Initialize(direction);
            projectile.speed = projectileSpeed;
            projectile.damage = BaseDamage;
            projectile.explosionRadius = explosionRadius;

            // Notify subscribers (like ForkSupportGem) that a projectile was spawned
            OnProjectileSpawned?.Invoke(projectile);
        }
    }
}