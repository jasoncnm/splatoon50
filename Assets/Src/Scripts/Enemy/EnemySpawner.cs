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

    [Header("Cluster spawning")]
    [Tooltip("Radius (world units) around the picked centre in which to scatter this batch.")]
    [SerializeField, Min(0f)] private float spawnRadius = 2f;

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
        UpdateVisiblePositions();
        if (_visiblePositions.Count == 0)
        {
            Debug.Log("Spawner: no walkable tiles inside the camera right now.");
            return;
        }

        Vector3 centre = _visiblePositions[Random.Range(0, _visiblePositions.Count)];

        int spawned = 0;
        int safety = batchSize * 3; // avoid infinite loops if map is tiny
        while (spawned < batchSize && safety-- > 0)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector3 tryPos = centre + (Vector3)offset;

            if (!IsSpawnable(tryPos)) continue; // reject if off-screen or not on ground

            GameObject pf = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(pf, tryPos, Quaternion.identity);
            spawned++;
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

    private void UpdateVisiblePositions()
    {
        _visiblePositions.Clear();
        foreach (Vector3 worldPos in _validPositions)
            if (IsOnScreen(worldPos))
                _visiblePositions.Add(worldPos);
    }

    private bool IsOnScreen(Vector3 worldPos)
    {
        Vector3 vp = targetCamera.WorldToViewportPoint(worldPos);
        return vp.z > 0f && vp.x > 0f && vp.x < 1f && vp.y > 0f && vp.y < 1f;
    }

    private bool IsSpawnable(Vector3 worldPos)
    {
        // 1. must still be inside the camera
        if (!IsOnScreen(worldPos)) return false;
        // 2. must fall on a tile that exists in the ground tilemap
        return groundTilemap.HasTile(groundTilemap.WorldToCell(worldPos));
    }
}
