using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public GameObject zonePrefab;

    [SerializeField] private Player player;  // Make private and use a property for access
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Zone currentZone;

    public Player Player  // Public property to access the player
    {
        get { return player; }
    }

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
        //uiManager.Initialize(player);
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
