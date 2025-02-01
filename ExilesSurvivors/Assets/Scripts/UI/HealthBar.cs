using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Image healthFill; // Reference to the UI Image for health bar

    public void Initialize(Player player)
    {
        Debug.Log("HealthBar initialized!");
        // Add logic to update the health bar based on player's health
    }

    public void UpdateHealth(float health)
    {
        Debug.Log($"Updating health bar to {health}!");
        healthFill.fillAmount = health / 100f; // Example: Update the health bar fill amount
    }
}