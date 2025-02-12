using UnityEngine;
using System.Collections.Generic;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform healthBarCanvas;

    private Dictionary<Character, UIPoolBar> healthBars = new Dictionary<Character, UIPoolBar>();
    private Dictionary<Character, Transform> trackedTargets = new Dictionary<Character, Transform>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        UpdateHealthBarPositions();
    }

    public void RemoveHealthBar(Character character)
    {
        if (healthBars.ContainsKey(character))
        {
            Destroy(healthBars[character].gameObject);
            healthBars.Remove(character);
            trackedTargets.Remove(character);
        }
    }


    public void AddHealthBar(Character character, Transform target)
    {
        if (healthBars.ContainsKey(character)) return;

        if (healthBarPrefab == null || healthBarCanvas == null || character == null)
        {
            Debug.LogError("HealthBarManager references are not assigned!");
            return;
        }

        GameObject healthBarObj = Instantiate(healthBarPrefab, healthBarCanvas);
        UIPoolBar healthBar = healthBarObj.GetComponent<UIPoolBar>();
        healthBar.Show(character.LifePool);

        healthBars.Add(character, healthBar);
        trackedTargets.Add(character, target); // Store the character's transform
    }

    private void UpdateHealthBarPositions()
    {
        foreach (var pair in healthBars)
        {
            Character character = pair.Key;
            UIPoolBar healthBar = pair.Value;

            if (character == null || !trackedTargets.ContainsKey(character))
            {
                continue;
            }

            Transform targetTransform = trackedTargets[character];

            // Adjusted position to be closer to the character’s head
            Vector3 worldPosition = targetTransform.position + new Vector3(0, 1.0f, 0);
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            healthBar.transform.position = screenPosition;
        }
    }

}
