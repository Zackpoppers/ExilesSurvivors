using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Health = 100f;
    public float Armor = 10f;
    public float MovementSpeed = 5f;
    public List<SkillGem> ActiveSkills = new List<SkillGem>();
    public Inventory Inventory;

    private bool _supportsApplied;

    private void Start()
    {
        ApplyAllSupports();
    }

    private void ApplyAllSupports()
    {
        if (_supportsApplied) return;

        foreach (var skill in ActiveSkills)
        {
            foreach (var support in skill.SupportGems)
            {
                support.ApplySupport(skill);
            }
        }
        _supportsApplied = true;
    }

    private void OnEnable()
    {
        // Reset when entering play mode
        _supportsApplied = false;
    }

    private void HandleSkills()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            ActiveSkills[0].Activate(this);
            

        }
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

    

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        GameManager.Instance.OnPlayerDeath();
    }
}