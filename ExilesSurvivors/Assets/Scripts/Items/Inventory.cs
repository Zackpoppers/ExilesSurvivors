using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [SerializeField] int GridSize = 20;
    [SerializeField] List<Item> Items = new List<Item>();

    public void AddItem(Item item)
    {
        
        if (Items.Count < GridSize)
        {
            Items.Add(item);
            Debug.Log($"Added {item.Name} to inventory!");
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        Debug.Log($"Removed {item.Name} from inventory!");
    }
        
}