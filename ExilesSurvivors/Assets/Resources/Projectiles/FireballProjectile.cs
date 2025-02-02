using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 10f;
    public float damage = 20f;
    public float explosionRadius = 1f;

    private Rigidbody2D _rb;
    private Vector2 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _direction * speed; // Set initial velocity
    }

    public void Initialize(Vector2 direction)
    {
        _direction = direction.normalized;
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
        // Damage enemies in radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage);
                }
            }
        }
    }
}