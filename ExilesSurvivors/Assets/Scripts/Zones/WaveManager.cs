using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int CurrentWave = 1;
    public float EnemySpawnRate = 5f;
    public int BossSpawnInterval = 5;

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
        Debug.Log($"Spawning enemies for wave {CurrentWave}!");
    }

    private void SpawnBoss()
    {
        Debug.Log($"Spawning boss for wave {CurrentWave}!");
    }

    private void IncreaseDifficulty()
    {
        Debug.Log($"Increasing difficulty for wave {CurrentWave}!");
    }
}