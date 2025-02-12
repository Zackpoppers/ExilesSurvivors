using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public interface IExplodable
{
    event Action<Vector2, Vector2, Projectile> OnExplode;
}

public class FireballProjectile : Projectile, IExplodable
{
    public event Action<Vector2, Vector2, Projectile> OnExplode;
    public float ExplosionRadius;
    public GameObject ExplosionEffectPrefab; // Add this property

    protected override void HandleDefaultHit(Collider2D other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("Terrain")) && !HitEnemies.Contains(other.gameObject))
        {
            HitEnemies.Add(other.gameObject);
            Explode();
            OnDestroy();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        OnExplode?.Invoke(transform.position, direction, this);

        // Create explosion effect
        if (ExplosionEffectPrefab != null)
        {
            var explosion = Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
            explosion.transform.SetParent(GameObject.Find("Projectiles").transform);
            Destroy(explosion, 2f); // Adjust duration to match your animation
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") && hit.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage((int)Damage);
            }
        }
    }

}