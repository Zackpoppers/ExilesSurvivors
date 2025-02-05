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
        projectile.OnDestroyed += ReturnProjectile;
    }

    private void ReturnProjectile(Projectile original)
    {
        var newProjectile = Instantiate(original.gameObject, original.rb.position, Quaternion.identity)
                 .GetComponent<Projectile>();

        newProjectile.Initialize(original.direction *-1, original.Speed, original.Damage);
        newProjectile.HitEnemies.UnionWith(original.HitEnemies);
    }
}