using UnityEngine;

[CreateAssetMenu(fileName = "ForkSupportGem", menuName = "Gems/SupportGem/Fork")]
public class ForkSupportGem : SupportGem
{
    [Header("Fork Settings")]
    public int forkCount = 2; // Number of projectiles to spawn
    public float forkAngle = 45f; // Angle between projectiles

    public override void ApplySupport(Skill targetSkill)
    {
        if (targetSkill is FireballGem fireball)
        {
            // Hook into the Fireball's explosion event
            fireball.OnProjectileSpawned += AddForkBehavior;
        }
    }

    private void AddForkBehavior(FireballProjectile projectile)
    {
        // Subscribe to the projectile's explosion event
        projectile.OnExplode += ForkProjectiles;
    }

    private void ForkProjectiles(Vector2 position, Vector2 originalDirection, GameObject projectileToFork)
    {
        for (int i = 0; i < forkCount; i++)
        {
            // Calculate the new direction for each fork
            float angle = -forkAngle / 2 + i * (forkAngle / (forkCount - 1));
            Vector2 forkDirection = Quaternion.Euler(0, 0, angle) * originalDirection;

            // Spawn a new projectile
            FireballProjectile newProjectile = Instantiate(
                projectileToFork, // You'll need a reference to the original Fireball prefab
                position,
                Quaternion.identity
            ).GetComponent<FireballProjectile>();

            newProjectile.Initialize(forkDirection);
            
        }
    }
}