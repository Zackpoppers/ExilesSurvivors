using UnityEngine;

[CreateAssetMenu(fileName = "ReturnSupportGem", menuName = "Gems/SupportGem/Return")]
public class ReturnSupportGem : SupportGem
{
    public override void ApplySupport(SkillGem skill)
    {
        skill.OnProjectileSpawned += HandleProjectileSpawned;
    }

    private void HandleProjectileSpawned(Projectile projectile)
    {
        if (projectile is IExplodable explodable)
        {
            explodable.OnExplode += ReturnProjectile;
        }
    }

    private void ReturnProjectile(Vector2 position, Vector2 direction, Projectile original)
    {
        var newProjectile = Instantiate(original.gameObject, position, Quaternion.identity)
                 .GetComponent<Projectile>();

        newProjectile.Initialize(direction*-1, original.Speed, original.Damage);
        newProjectile.HitEnemies.UnionWith(original.HitEnemies);
    }
}