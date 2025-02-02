using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Health = 100f;
    public float Armor = 10f;
    public float MovementSpeed = 5f;
    public List<Skill> ActiveSkills = new List<Skill>();
    public Inventory Inventory;

    private void Start()
    {
        ActiveSkills[0].SupportGems[0].ApplySupport(ActiveSkills[0]);
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && ActiveSkills.Count > 0)
        {
            
            //ActiveSkills[0].SupportGems[1].ApplySupport(ActiveSkills[0]);

            ActiveSkills[0].Activate(this);
            
        }
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