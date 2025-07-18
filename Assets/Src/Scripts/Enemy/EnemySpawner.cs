using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int enemiesPerBatch = 2;
    
    [Header("Tilemap Settings")]
    public Tilemap spawnTilemap;
    public TileBase[] spawnableTiles;

    public LayerMask backgroundMask;

    private Camera gameCamera;
    private float spawnTimer;

    void Start()
    {
        if (spawnTilemap == null)
        {
            Debug.LogError("Spawn Tilemap not assigned!");
            return;
        }

        CacheValidSpawnPositions();
        spawnTimer = spawnInterval;  // Spawn first batch immediately
    }

    void Update()
    {
        if (validSpawnPositions.Count == 0) return;
        
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemyBatch();
            spawnTimer = 0f;
        }
    }

    void CacheValidSpawnPositions()
    {
        // Spawn first enemy

        Instantiate(enemyPrefab, GetSpawnablePosition(), Quaternion.identity);

    }

    Vector2 GetSpawnablePosition()
    {
        Vector2 position = GetRandomSpawnPosition();

        while (!CanSpawn(position)) position = GetRandomSpawnPosition(); ;

        return position;

    }

    bool CanSpawn(Vector2 pos)
    {
        bool result = true;

        Collider2D collider = Physics2D.OverlapBox(pos, enemyPrefab.GetComponent<Collider2D>().bounds.extents, 0, backgroundMask);

        if (collider)
        {
            result = false;
        }

        return result;
        
    }

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (spawnTilemap.HasTile(position))
            {
                TileBase tile = spawnTilemap.GetTile(position);
                
                foreach (TileBase spawnableTile in spawnableTiles)
                {
                    if (tile == spawnableTile)
                    {
                        Vector3 worldPosition = spawnTilemap.GetCellCenterWorld(position);
                        validSpawnPositions.Add(worldPosition);
                        break;
                    }
                }
            }
        }

        if (validSpawnPositions.Count == 0)
        {
            Debug.LogWarning("No valid spawn positions found! Check your tilemap and spawnable tiles.");
        }
    }

    void SpawnEnemyBatch()
    {
        if (validSpawnPositions.Count == 0 || enemiesPerBatch <= 0) return;
        
        // Create a list to track positions we've used this batch
        List<Vector3> usedPositions = new List<Vector3>();
        
        for (int i = 0; i < Mathf.Min(enemiesPerBatch, validSpawnPositions.Count); i++)
        {
            Vector3 spawnPosition;
            int attempts = 0;
            const int maxAttempts = 10;  // Prevent infinite loops
            
            // Try to find a unique position
            do
            {
                spawnPosition = validSpawnPositions[Random.Range(0, validSpawnPositions.Count)];
                attempts++;
            } 
            while (usedPositions.Contains(spawnPosition) && attempts < maxAttempts);
            
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            usedPositions.Add(spawnPosition);
        }
        
        // If we need more enemies than available positions, spawn duplicates
        if (enemiesPerBatch > validSpawnPositions.Count)
        {
            int extraSpawns = enemiesPerBatch - validSpawnPositions.Count;
            for (int i = 0; i < extraSpawns; i++)
            {
                Vector3 spawnPosition = validSpawnPositions[Random.Range(0, validSpawnPositions.Count)];
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}