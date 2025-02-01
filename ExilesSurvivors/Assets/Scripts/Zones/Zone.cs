using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [Header("Zone Settings")]
    public string theme = "Forest"; // Zone theme (e.g., Forest, Desert, Dungeon)
    public int size = 50; // Size of the zone (width and height)
    public int maxWalls = 10; // Maximum number of walls to spawn
    public int maxEnemies = 5; // Maximum number of enemies to spawn
    public GameObject wallPrefab; // Prefab for walls
    public GameObject enemyPrefab; // Prefab for enemies

    private List<Vector3> enemySpawnPoints = new List<Vector3>();

    public void Generate()
    {
        Debug.Log($"Generating {theme} zone...");

        // Clear existing walls and enemies
        ClearZone();

        // Generate walls
        GenerateWalls();

        // Generate enemy spawn points
        GenerateEnemySpawnPoints();

        // Spawn enemies
        SpawnEnemies();
    }

    private void ClearZone()
    {
        // Destroy all walls and enemies in the zone
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        enemySpawnPoints.Clear();
    }

    private void GenerateWalls()
    {
        for (int i = 0; i < maxWalls; i++)
        {
            // Randomize wall position within the zone
            Vector3 wallPosition = new Vector3(
                Random.Range(-size / 2, size / 2),
                Random.Range(-size / 2, size / 2),
                0
            );

            // Instantiate wall
            GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity, transform);
            wall.name = $"Wall_{i}";
        }
    }

    private void GenerateEnemySpawnPoints()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            // Randomize enemy spawn position within the zone
            Vector3 spawnPosition = new Vector3(
                Random.Range(-size / 2, size / 2),
                Random.Range(-size / 2, size / 2),
                0
            );

            // Add spawn position to the list
            enemySpawnPoints.Add(spawnPosition);
        }
    }

    private void SpawnEnemies()
    {
        foreach (Vector3 spawnPosition in enemySpawnPoints)
        {
            // Instantiate enemy at spawn position
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
            enemy.name = "Enemy";
        }
    }

    public void ChangeTheme(string newTheme)
    {
        theme = newTheme;
        Debug.Log($"Zone theme changed to {theme}!");
        // Add logic to update visuals based on the new theme (e.g., change background, walls, etc.)
    }
}