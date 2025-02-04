using UnityEngine;

public interface IProjectile
{
    void Initialize(Vector2 direction);
    GameObject GetPrefab(); // Optional: If you need access to the prefab
}