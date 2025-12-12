using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour
{

    public EnemiesSO enemiesList;

    [Header("Scene references")]
    [SerializeField] private Tilemap groundTilemap;
    [Tooltip("If left null, Camera.main is used.")]
    [SerializeField] private Camera targetCamera;

    [Header("Spawn timing")]
    [SerializeField, Min(1)] private int batchSize = 5;
    [SerializeField, Min(0f)] private float spawnInterval = 3f;
    [SerializeField] private bool startOnAwake = true;

    [Header("Cluster spawning")]
    [Tooltip("Radius (world units) around the picked centre in which to scatter this batch.")]
    [SerializeField, Min(0f)] private float spawnRadius = 2f;

    [Header("Indicator")]
    [SerializeField] Transform indicator;

    private Transform[] enemyPrefabs;
    private readonly List<Vector3> _validPositions = new();
    private readonly List<Vector3> _visiblePositions = new(); // walkable + on-screen
    private Coroutine _loop;


    Transform[] indicators;
    Transform[] enemyBuffer;
    Vector3[] enemiesPos;
    List<GameObject> enemies;

    private void Awake()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        CacheTileCenters();
        enemies = new List<GameObject>();
        indicators = new Transform[batchSize];

    }

    void Update()
    {
        if (enemyBuffer == null)
            enemyBuffer = GetEnemiesKind(batchSize);
        if (enemiesPos == null)
            enemiesPos = GetSpawnBatchLocations(enemyBuffer);
    }
    
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => enemiesPos != null);

            for (int i = 0; i < batchSize; i++)
            {
                indicators[i] = Instantiate(indicator, enemiesPos[i], Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval);

            for (int i = 0; i < batchSize; i++)
            {
                if (indicators == null || enemiesPos == null)
                {
                    Assert.IsTrue(false, "indicators or enimesPos are not initialized");
                    break;
                }

                Destroy(indicators[i].gameObject);

                Transform pf = enemyBuffer[i];
                Transform tr = Instantiate(pf, enemiesPos[i], Quaternion.identity);
                enemies.Add(tr.gameObject);
            }

            enemyBuffer = null;
            enemiesPos = null;

        }
    }

    private void OnDisable()
    {
        if (_loop != null) StopCoroutine(_loop);

        if (enemies != null)
        {
            foreach ( GameObject e in enemies )
            {
                if (e) e.GetComponent<Enemy>().StartDie();
            }
        }

        if (indicators != null)
        {
            foreach ( Transform i in indicators)
            {
                if (i) Destroy(i.gameObject);
            }
        }

    }

    public void SetUp()
    {
        if (enemyPrefabs == null) enemyPrefabs = enemiesList.enemies;
        if (spawnInterval > 0f)
            _loop = StartCoroutine(SpawnLoop());
    }

    public Vector3[] GetSpawnBatchLocations(Transform[] enemies)
    {
        UpdateVisiblePositions();

        if (_visiblePositions.Count == 0)
        {
            Debug.Log("Spawner: no walkable tiles inside the camera right now.");
            return null;
        }


        Vector3[] result = new Vector3[batchSize];


        Vector3 centre = _visiblePositions[Random.Range(0, _visiblePositions.Count)];

        int spawned = 0;
        int safety = batchSize * 3; // avoid infinite loops if map is tiny
        while (spawned < batchSize && safety-- > 0)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector3 tryPos = centre + (Vector3)offset;

            if (!IsSpawnable(tryPos, result, enemies, spawned)) continue; // reject if off-screen or not on ground

            result[spawned] = tryPos;

            spawned++;
        }
        return result;
    }

    public Transform[] GetEnemiesKind(int count)
    {
        Transform[] result = new Transform[batchSize];

        for (int i = 0; i < count; i++)
        {
            result[i] = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }
        return result;
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

    private bool IsSpawnable(Vector3 worldPos, Vector3[] enemyPos, Transform[] enemies, int length)
    {
        // 1. must still be inside the camera
        if (!IsOnScreen(worldPos)) return false;
        
        // 2. must fall on a tile that exists in the ground tilemap
        if (!groundTilemap.HasTile(groundTilemap.WorldToCell(worldPos))) return false;
        
        // 3. must not spawn on top of perivous spawned enemies
        for (int i = 0; i < length; i++)
        {
            Vector3 pos = enemyPos[i];
            float enemyR = enemies[i].localScale.x * 0.5f;
            float currentR = enemies[length].localScale.x * 0.5f;

            float dist = Vector3.Distance(pos, worldPos);

            if (dist < (currentR + enemyR)) return false;

        }

        return true;

    }

    private void OnDrawGizmos()
    {
        //if (!Application.isPlaying) return;
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(Vector3.zero, enemyPrefabs[0].localScale.x * 0.5f);
    }

}
