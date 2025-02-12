using UnityEngine;

[CreateAssetMenu(fileName = "PierceSupportGem", menuName = "Gems/SupportGem/Pierce")]
public class PierceSupportGem : SupportGem
{
    protected override void HandleProjectileSpawned(Projectile projectile)
    {
        projectile.OnDestroyed += Pierce;
    }

    private void Pierce(Projectile original)
    {
        var newProjectile = Instantiate(original.gameObject, original.rb.position, Quaternion.identity)
                 .GetComponent<Projectile>();

        newProjectile.Initialize(original.direction, original.Speed, original.Damage);
        //newProjectile.CopyProjectileEvents(original);

        newProjectile.HitEnemies.UnionWith(original.HitEnemies);

    }
}