using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Projectile : MonoBehaviour
{
    public event EventHandler<ProjectileHitEventArgs> OnHit;
    public event Action<Projectile> OnCreated;
    public event Action<Projectile> OnDestroyed;

    public float Speed { get; set; }
    public float Damage { get; set; }
    public HashSet<GameObject> HitEnemies { get; } = new HashSet<GameObject>();

    public Rigidbody2D rb;
    public Vector2 direction;
    public GameObject prefab;

    public virtual void Initialize(Vector2 direction, float speed, float damage)
    {

        //parent the projectile game object to a folder called projectiles
        gameObject.transform.SetParent(GameObject.Find("Projectiles").transform);
        this.direction = direction;
        Speed = speed;
        Damage = damage;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * Speed;
        OnCreated?.Invoke(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BoundingBox")){
            Destroy(gameObject);
        }

        var args = new ProjectileHitEventArgs(other);
        OnHit?.Invoke(this, args);

        if (!args.CancelDefaultAction)
        {
            HandleDefaultHit(other);
        }
    }

    protected virtual void HandleDefaultHit(Collider2D other)
    {
        if (other.CompareTag("Enemy")) Destroy(gameObject);
    }

    protected virtual void OnDestroy() => OnDestroyed?.Invoke(this);

    protected virtual void OnDisable()
    {
        // Clear event listeners when destroyed
        OnHit = null;
        OnCreated = null;
        OnDestroyed = null;
    }

    public virtual void CopyProjectileEvents(Projectile ProjectileToCopyFrom) {

        OnDestroyed += ProjectileToCopyFrom.OnDestroyed;
        OnCreated += ProjectileToCopyFrom.OnCreated;
        OnHit += ProjectileToCopyFrom.OnHit;
    
    }
}

public class ProjectileHitEventArgs : EventArgs
{
    public Collider2D Collider { get; }
    public bool CancelDefaultAction { get; set; }

    public ProjectileHitEventArgs(Collider2D collider)
    {
        Collider = collider;
        CancelDefaultAction = false;
    }
}