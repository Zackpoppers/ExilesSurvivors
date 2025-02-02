using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int CurrentWave = 1;
    public float EnemySpawnRate = 5f;
    public int BossSpawnInterval = 5;
    public int InitialEnemyCount = 5;
    public float SpawnDistance = 20f;  // Distance from the player to spawn enemies
    public GameObject EnemyPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartWaves()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            SpawnEnemies();
            if (CurrentWave % BossSpawnInterval == 0) SpawnBoss();
            CurrentWave++;
            IncreaseDifficulty();
            yield return new WaitForSeconds(EnemySpawnRate);
        }
    }

    private void SpawnEnemies()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        int enemyCount = Mathf.FloorToInt(InitialEnemyCount + CurrentWave * 0.5f);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * SpawnDistance;
            spawnDirection.z = 0; // Ensure it's a 2D spawn if your game is 2D
            Vector3 spawnPos = playerPos + spawnDirection;

            GameObject enemyObject = Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            // Set enemy tier based on the current wave
            ConfigureEnemyTier(enemy, CurrentWave);

            Debug.Log($"Spawning enemy {i + 1} for wave {CurrentWave} at {spawnPos}!");
        }
    }

    private void SpawnBoss()
    {
        Debug.Log($"Spawning boss for wave {CurrentWave}!");
    }

    private void IncreaseDifficulty()
    {
        Debug.Log($"Increasing difficulty for wave {CurrentWave}!");
        EnemySpawnRate *= 0.95f;  // Decrease spawn interval to make the game harder
    }

    private void ConfigureEnemyTier(Enemy enemy, int wave)
    {

    }
}
