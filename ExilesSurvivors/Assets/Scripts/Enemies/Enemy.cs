using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private EnemyTier tier = EnemyTier.Normal;
    [SerializeField] private List<Modifier> modifiers = new List<Modifier>();

    private Transform playerTransform;

    private void Start()
    {
        // Assume GameManager has a static reference to the Player
        playerTransform = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * movementSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Enemy Died!");
        Destroy(gameObject);
    }
}

public enum EnemyTier
{
    Normal,
    Magic,
    Rare
}
