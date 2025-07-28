using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Scene references")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Spawn timing")]
    [SerializeField, Min(1)] private int batchSize = 5;
    [SerializeField, Min(0f)] private float spawnInterval = 3f;
    [SerializeField] private bool startOnAwake = true;

    private readonly List<Vector3> _validPositions = new();
    private Coroutine _loop;

    private void Awake()  => CacheTileCenters();

    private void Start()
    {
        if (startOnAwake && spawnInterval > 0f)
            if () {
                _loop = StartCoroutine(SpawnLoop());
            }
        // else if (startOnAwake)                 // one-off burst
        //     SpawnBatch();
    }

    private void OnDisable()
    {
        if (_loop != null) StopCoroutine(_loop);
    }

    public void SpawnBatch()
    {
        if (_validPositions.Count == 0)
        {
            Debug.LogWarning("Spawner: no valid tiles found.");
            return;
        }

        for (int i = 0; i < batchSize; i++)
        {
            Vector3 pos  = _validPositions[Random.Range(0, _validPositions.Count)];
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
}
