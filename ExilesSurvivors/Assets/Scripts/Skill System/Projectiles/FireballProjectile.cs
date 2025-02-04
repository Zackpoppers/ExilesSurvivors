using System;
using UnityEngine;

public interface IExplodable
{
    event Action<Vector2, Vector2, Projectile> OnExplode;
}

public class FireballProjectile : Projectile, IExplodable
{
    public event Action<Vector2, Vector2, Projectile> OnExplode;
    public float ExplosionRadius;

    protected override void HandleDefaultHit(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !HitEnemies.Contains(other.gameObject))
        {
            HitEnemies.Add(other.gameObject);
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        OnExplode?.Invoke(transform.position, direction, this);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") && hit.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}