using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "MultipleProjectilesSupport", menuName = "Gems/SupportGem/MultipleProjectiles")]
public class MultipleProjectilesSupport : SupportGem
{

    [SerializeField] float totalSpread = 40f;
    [SerializeField] float numberOfProjectiles = 4;

    public override void ApplySupport(SkillGem skill)
    {
        skill.OnProjectileSpawned += DuplicateProjectile;
    }

    private void HandleProjectileSpawned(Projectile projectile)
    {
        projectile.OnCreated += DuplicateProjectile;
    }

    private void DuplicateProjectile(Projectile original)
    {

        
        Vector2 baseDirection = original.direction.normalized;

        for (int i = 1; i < 5; i++)
        {
            // Calculate angle offset (-30°, -10°, +10°, +30°)
            float angleOffset = -totalSpread / 2 + (i - 1) * (totalSpread / (numberOfProjectiles - 1));

            // Calculate rotated direction
            Vector2 spreadDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

            // Calculate proper rotation for the projectile sprite
            float projectileAngle = Mathf.Atan2(spreadDirection.y, spreadDirection.x) * Mathf.Rad2Deg;

            var projectileObj = Instantiate(
                original.prefab,
                original.rb.position,
                Quaternion.Euler(0, 0, projectileAngle)
            );

            if (projectileObj.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.Initialize(spreadDirection, original.Speed/2.8f, original.Damage);

            }
        }
    }
}