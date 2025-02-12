using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Character character; // Reference to the Character component
    public float MovementSpeed = 5f;
    public List<SkillGem> ActiveSkills;
    public Inventory Inventory;

    private void Start()
    {
        // Get the Character component
        character = GetComponent<Character>();

        if (character == null)
        {
            Debug.LogError("Character component missing on Player!");
            return;
        }

        // Register with the HealthBarManager
        HealthBarManager.Instance.AddHealthBar(character, transform);
        ApplyAllSupports();
    }

    private void Update()
    {
        HandleMovement();
        HandleSkills();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, moveY, 0));
    }

    private void HandleSkills()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActiveSkills[0].Activate(this);
        }
    }

    public void TakeDamage(float damage)
    {
        // Use the Character's TakeDamage method for centralized damage calculations
        character.TakeDamage((int)damage);

        // Check if the player has died
        if (character.LifePool.currentValue <= 0)
        {
            Die();
        }
    }
    private void ApplyAllSupports()
    {
        
        foreach (var skill in ActiveSkills)
        {
            foreach (var support in skill.SupportGems)
            {
                support.ApplySupport(skill);
            }
        }
        
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        GameManager.Instance.OnPlayerDeath();
    }

    // Example method to heal the player
    public void Heal(int amount)
    {
        character.LifePool.currentValue = Mathf.Min(character.LifePool.currentValue + amount, character.LifePool.maxValue.value);
        Debug.Log("Player healed. Current Health: " + character.LifePool.currentValue);
    }
}