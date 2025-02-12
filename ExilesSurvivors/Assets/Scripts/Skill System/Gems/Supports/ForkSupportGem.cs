using UnityEngine;

[CreateAssetMenu(fileName = "ForkSupportGem", menuName = "Gems/SupportGem/Fork")]
public class ForkSupportGem : SupportGem
{
    public int forkCount = 2;
    public float forkAngle = 45f;

    

    protected override void HandleProjectileSpawned(Projectile projectile)
    {
        projectile.OnDestroyed += ForkProjectile;
    }

    private void ForkProjectile(Projectile original)
    {
        for (int i = 0; i < forkCount; i++)
        {
            float angle = -forkAngle / 2 + i * (forkAngle / (forkCount - 1));
            Vector2 newDir = Quaternion.Euler(0, 0, angle) * original.direction;

            var newProjectile = Instantiate(original.gameObject, original.rb.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.Initialize(newDir, original.Speed, original.Damage);
            //newProjectile.CopyProjectileEvents(original); Add back in when we find a way to remove the fork support off the copy

            newProjectile.HitEnemies.UnionWith(original.HitEnemies);

        }
    }
}