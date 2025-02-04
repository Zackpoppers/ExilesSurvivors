using UnityEngine;

[CreateAssetMenu(fileName = "ForkSupportGem", menuName = "Gems/SupportGem/Fork")]
public class ForkSupportGem : SupportGem
{
    public int forkCount = 2;
    public float forkAngle = 45f;

    public override void ApplySupport(SkillGem skill)
    {
        skill.OnProjectileSpawned += HandleProjectileSpawned;
    }

    private void HandleProjectileSpawned(Projectile projectile)
    {
        if (projectile is IExplodable explodable)
        {
            explodable.OnExplode += ForkProjectile;
        }
    }

    private void ForkProjectile(Vector2 position, Vector2 direction, Projectile original)
    {
        for (int i = 0; i < forkCount; i++)
        {
            float angle = -forkAngle / 2 + i * (forkAngle / (forkCount - 1));
            Vector2 newDir = Quaternion.Euler(0, 0, angle) * direction;

            var newProjectile = Instantiate(original.gameObject, position, Quaternion.identity)
                .GetComponent<Projectile>();

            newProjectile.Initialize(newDir, original.Speed, original.Damage);
            newProjectile.HitEnemies.UnionWith(original.HitEnemies);
        }
    }
}