using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] HealthBar healthBar;
    [SerializeField] SkillHotbar skillHotbar;
    [SerializeField] InventoryUI inventoryUI; // Ensure this matches the class name
    public ItemGrid selectedItemGrid;

    private void Update()
    {
        if (selectedItemGrid == null) { return; }

        selectedItemGrid.GetTileGridPosition(Input.mousePosition);
        Debug.Log(selectedItemGrid.GetTileGridPosition(Input.mousePosition));
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Initialize(Player player)
    {
        
        healthBar.Initialize(player);
        skillHotbar.Initialize(player);
        inventoryUI.Initialize(player.Inventory); // Initialize the inventory UI
        
    }
}