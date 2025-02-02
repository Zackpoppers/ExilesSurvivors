using UnityEngine;

[CreateAssetMenu(fileName = "FireballGem", menuName = "Gems/SkillGem/Fireball")]
public class FireballGem : Skill
{
    [Header("Fireball Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float explosionRadius = 1f;

    public override void Activate(Player player)
    {
        if (projectilePrefab == null) return;

        // Get mouse position in world (2D)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Ensure Z=0 for 2D

        // Calculate direction from player to mouse
        Vector2 direction = (mouseWorldPos - player.transform.position).normalized;

        // Spawn projectile
        GameObject fireball = Instantiate(
            projectilePrefab,
            player.transform.position,
            Quaternion.identity
        );

        // Initialize projectile
        FireballProjectile projectile = fireball.GetComponent<FireballProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(direction);
            projectile.speed = projectileSpeed;
            projectile.damage = BaseDamage;
            projectile.explosionRadius = explosionRadius;
        }

    }
}