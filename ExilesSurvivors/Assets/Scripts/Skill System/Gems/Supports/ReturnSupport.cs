using UnityEngine;

[CreateAssetMenu(fileName = "ReturnSupportGem", menuName = "Gems/SupportGem/Return")]
public class ReturnSupportGem : SupportGem
{
 
    protected override void HandleProjectileSpawned(Projectile projectile)
    {
        projectile.OnDestroyed += ReturnProjectile;
    }

    private void ReturnProjectile(Projectile original)
    {
        var newProjectile = Instantiate(original.gameObject, original.rb.position, Quaternion.identity)
                 .GetComponent<Projectile>();

        newProjectile.Initialize(original.direction *-1, original.Speed, original.Damage);
        //newProjectile.CopyProjectileEvents(original);

        newProjectile.HitEnemies.UnionWith(original.HitEnemies);

    }
}