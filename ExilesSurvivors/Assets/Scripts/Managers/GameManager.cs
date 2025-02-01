using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public GameObject zonePrefab;

    [SerializeField] Player player;
    [SerializeField] WaveManager waveManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] Zone currentZone;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartGame(); // Call StartGame for testing
    }

    public void StartGame()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        currentZone = Instantiate(zonePrefab, Vector3.zero, Quaternion.identity).GetComponent<Zone>();
        currentZone.Generate(); // Generate the zone
        waveManager.StartWaves();
        uiManager.Initialize(player);
    }

    public void EndGame()
    {
        Debug.Log("Game Over!");
        // Clean up and reset game state
    }

    public void OnPlayerDeath()
    {
        EndGame();
    }
}