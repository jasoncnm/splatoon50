using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Scene references")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private GameObject[] enemyPrefabs;
    [Tooltip("If left null, Camera.main is used.")]
    [SerializeField] private Camera targetCamera;

    [Header("Spawn timing")]
    [SerializeField, Min(1)] private int batchSize = 5;
    [SerializeField, Min(0f)] private float spawnInterval = 3f;
    [SerializeField] private bool startOnAwake = true;

    private readonly List<Vector3> _validPositions = new();
    private readonly List<Vector3> _visiblePositions = new(); // walkable + on-screen
    private Coroutine _loop;

    private void Awake()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        CacheTileCenters();
    }

    private void Start()
    {
        if (startOnAwake && spawnInterval > 0f)
            _loop = StartCoroutine(SpawnLoop());
        // else if (startOnAwake)
        //     SpawnBatch();
    }

    private void OnDisable()
    {
        if (_loop != null) StopCoroutine(_loop);
    }

    public void SpawnBatch()
    {
        // Refresh the “on-screen” buffer every batch so it tracks camera movement
        UpdateVisiblePositions();
        if (_visiblePositions.Count == 0)
        {
            Debug.Log("Spawner: no walkable tiles inside the camera right now.");
            return;
        }

        for (int i = 0; i < batchSize; i++)
        {
            Vector3 pos  = _visiblePositions[Random.Range(0, _visiblePositions.Count)];
            GameObject pf = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(pf, pos, Quaternion.identity);
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnBatch();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void CacheTileCenters()
    {
        _validPositions.Clear();
        foreach (Vector3Int cell in groundTilemap.cellBounds.allPositionsWithin)
            if (groundTilemap.HasTile(cell))
                _validPositions.Add(groundTilemap.GetCellCenterWorld(cell));
    }

    /// From the master list, keep only the cells that are inside the camera.
    private void UpdateVisiblePositions()
    {
        _visiblePositions.Clear();
        foreach (Vector3 worldPos in _validPositions)
        {
            Vector3 vp = targetCamera.WorldToViewportPoint(worldPos);
            bool onScreen = vp.z > 0f && vp.x > 0f && vp.x < 1f && vp.y > 0f && vp.y < 1f;
            if (onScreen) _visiblePositions.Add(worldPos);
        }
    }
}
