using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    // Event triggered when the fireball explodes
    public System.Action<Vector2, Vector2, GameObject> OnExplode;

    [Header("Settings")]
    public float speed = 10f;
    public float damage = 20f;
    public float explosionRadius = 1f;

    private Rigidbody2D _rb;
    private Vector2 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        _direction = direction;
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        // Trigger the explosion event
        OnExplode?.Invoke(transform.position, _direction, gameObject);

        // Damage enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}