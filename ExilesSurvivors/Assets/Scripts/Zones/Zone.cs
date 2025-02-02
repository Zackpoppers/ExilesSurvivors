using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [Header("Zone Settings")]
    public int size = 50; // Size of the zone (width and height)
    public float gridSpacing = 20f; // Distance between pathways (slightly larger than screen length)
    public int buildingsPerGrid = 1; // Number of buildings per grid cell
    public float boundaryRadius = 25f; // Radius of the circular boundary

    [Header("Prefabs")]
    public GameObject groundTilePrefab; // Prefab for ground tiles
    public GameObject pathTilePrefab; // Prefab for pathway tiles
    public GameObject buildingPrefab; // Prefab for buildings
    public GameObject wallPrefab; // Prefab for the boundary wall

    [Header("Ground Colors")]
    public Color[] groundColors; // Colors for the ground based on noise values

    [Header("Noise Settings")]
    public float noiseScale = 10f; // Scale for Perlin Noise

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        Debug.Log("Generating zone...");

        // Clear existing objects in the zone
        ClearZone();

        // Generate ground
        GenerateGround();

        // Generate pathways
        GeneratePathways();

        // Generate buildings
        GenerateBuildings();

        // Delete objects outside the boundary
        DeleteObjectsOutsideBoundary();

        // Build the circular boundary wall
        BuildBoundaryWall();
    }

    private void ClearZone()
    {
        // Destroy all objects in the zone
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateGround()
    {
        for (int x = -size / 2; x < size / 2; x++)
        {
            for (int y = -size / 2; y < size / 2; y++)
            {
                // Calculate Perlin Noise value for this position
                float perlinValue = Mathf.PerlinNoise((x + size) / noiseScale, (y + size) / noiseScale);

                // Determine ground color based on noise value
                Color groundColor = GetGroundColor(perlinValue);

                // Instantiate ground tile
                Vector3 groundPosition = new Vector3(x, y, 0);
                GameObject groundTile = Instantiate(groundTilePrefab, groundPosition, Quaternion.identity, transform);
                groundTile.name = $"Ground_{x}_{y}";

                // Set ground tile color
                SpriteRenderer groundRenderer = groundTile.GetComponent<SpriteRenderer>();
                if (groundRenderer != null)
                {
                    groundRenderer.color = groundColor;
                }
            }
        }
    }

    private Color GetGroundColor(float noiseValue)
    {
        // Ensure groundColors array is not empty
        if (groundColors == null || groundColors.Length == 0)
        {
            Debug.LogError("groundColors array is empty or not assigned!");
            return Color.white; // Default color
        }

        // Clamp the noiseValue to ensure it stays within the range [0, 1)
        noiseValue = Mathf.Clamp01(noiseValue);

        // Map the noise value to a color in the groundColors array
        int colorIndex = Mathf.FloorToInt(noiseValue * groundColors.Length);
        colorIndex = Mathf.Clamp(colorIndex, 0, groundColors.Length - 1);
        return groundColors[colorIndex];
    }

    private void GeneratePathways()
    {
        // Calculate the number of pathways based on grid spacing
        int pathwayCount = Mathf.FloorToInt(size / gridSpacing);

        for (int i = -pathwayCount; i <= pathwayCount; i++)
        {
            // Horizontal pathways
            for (int x = -size / 2; x < size / 2; x++)
            {
                Vector3 pathPosition = new Vector3(x, i * gridSpacing, 0);
                GameObject pathTile = Instantiate(pathTilePrefab, pathPosition, Quaternion.identity, transform);
                pathTile.name = $"Path_Horizontal_{i}_{x}";
            }

            // Vertical pathways
            for (int y = -size / 2; y < size / 2; y++)
            {
                Vector3 pathPosition = new Vector3(i * gridSpacing, y, 0);
                GameObject pathTile = Instantiate(pathTilePrefab, pathPosition, Quaternion.identity, transform);
                pathTile.name = $"Path_Vertical_{i}_{y}";
            }
        }
    }

    private void GenerateBuildings()
    {
        // Calculate the number of grid cells
        int gridCells = Mathf.FloorToInt(size / gridSpacing);

        // Track occupied positions to prevent overlap
        HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();

        for (int x = -gridCells; x < gridCells; x++)
        {
            for (int y = -gridCells; y < gridCells; y++)
            {
                // Randomly decide if a building should be placed in this grid cell
                if (Random.Range(0, 100) < buildingsPerGrid * 100)
                {
                    // Try to place a building in this grid cell
                    bool buildingPlaced = false;
                    int attempts = 0; // Limit the number of placement attempts

                    while (!buildingPlaced && attempts < 10) // Try up to 10 times
                    {
                        // Randomize building position within the grid cell
                        Vector3 buildingPosition = new Vector3(
                            x * gridSpacing + Random.Range(-gridSpacing / 2, gridSpacing / 2),
                            y * gridSpacing + Random.Range(-gridSpacing / 2, gridSpacing / 2),
                            0
                        );

                        // Round the position to the nearest integer to use as a key
                        Vector2Int roundedPosition = new Vector2Int(
                            Mathf.RoundToInt(buildingPosition.x),
                            Mathf.RoundToInt(buildingPosition.y)
                        );

                        // Check if the position is not on a pathway and not already occupied
                        if (!IsOnPathway(buildingPosition) && !occupiedPositions.Contains(roundedPosition))
                        {
                            // Place the building
                            GameObject building = Instantiate(buildingPrefab, buildingPosition, Quaternion.identity, transform);
                            building.name = $"Building_{x}_{y}";

                            // Mark the position as occupied
                            occupiedPositions.Add(roundedPosition);
                            buildingPlaced = true;
                        }

                        attempts++;
                    }

                    if (!buildingPlaced)
                    {
                        Debug.LogWarning($"Failed to place building in grid cell ({x}, {y}) after {attempts} attempts.");
                    }
                }
            }
        }
    }

    private bool IsOnPathway(Vector3 position)
    {
        // Check if the position is on a pathway
        return Mathf.Abs(position.x % gridSpacing) < 0.5f || Mathf.Abs(position.y % gridSpacing) < 0.5f;
    }

    private void DeleteObjectsOutsideBoundary()
    {
        // Iterate through all child objects in the zone
        foreach (Transform child in transform)
        {
            // Check if the object is outside the boundary radius
            if (Vector3.Distance(child.position, Vector3.zero) > boundaryRadius)
            {
                // Destroy the object
                Destroy(child.gameObject);
            }
        }
    }

    private void BuildBoundaryWall()
    {
        // Number of wall segments to create a smooth circle
        int wallSegments = 100;

        for (int i = 0; i < wallSegments; i++)
        {
            // Calculate the angle for this wall segment
            float angle = i * (360f / wallSegments);

            // Convert the angle to a position on the perimeter
            Vector3 wallPosition = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * boundaryRadius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * boundaryRadius,
                0
            );

            // Instantiate the wall segment
            GameObject wallSegment = Instantiate(wallPrefab, wallPosition, Quaternion.identity, transform);
            wallSegment.name = $"WallSegment_{i}";

            // Rotate the wall segment to face outward
            wallSegment.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
}