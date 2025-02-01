using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void Initialize(Inventory inventory)
    {
        Debug.Log("InventoryUI initialized!");
        // Add logic to display the inventory here
    }

    public void UpdateUI(List<Item> items)
    {
        Debug.Log("Updating Inventory UI!");
        // Add logic to update the UI with the latest items
    }
}