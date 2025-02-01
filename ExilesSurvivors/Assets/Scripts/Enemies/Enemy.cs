using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health = 50f;
    [SerializeField] EnemyTier Tier = EnemyTier.Normal;
    [SerializeField] List<Modifier> Modifiers = new List<Modifier>();

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) Die();
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