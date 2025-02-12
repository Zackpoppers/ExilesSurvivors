using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Character character;
    public float moveSpeed = 3f;  // Adjust as needed
    private Transform playerTransform;

    private void Start()
    {
        character = GetComponent<Character>();

        if (character == null)
        {
            Debug.LogError("Character component missing on Enemy!");
            return;
        }

        // Get the player's transform
        if (GameManager.Instance.Player != null)
        {
            playerTransform = GameManager.Instance.Player.transform;
        }
        else
        {
            Debug.LogError("Player reference missing in GameManager!");
        }

        // Register with the HealthBarManager
        HealthBarManager.Instance.AddHealthBar(character, transform);
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // Move toward the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        character.TakeDamage(damage);

        if (character.LifePool.currentValue <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died!");
        HealthBarManager.Instance.RemoveHealthBar(character);
        Destroy(gameObject);
    }
}
