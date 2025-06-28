using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    
    [Header("Spawn Area")]
    public bool useCameraBounds = true;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);  // Used if not using camera bounds

    public LayerMask backgroundMask;

    private Camera gameCamera;
    private float spawnTimer;

    void Start()
    {
        gameCamera = Camera.main;
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemyPair();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemyPair()
    {
        // Spawn first enemy

        Instantiate(enemyPrefab, GetSpawnablePosition(), Quaternion.identity);

        // Spawn second enemy at differen
        Instantiate(enemyPrefab, GetSpawnablePosition(), Quaternion.identity);
    }

    Vector2 GetSpawnablePosition()
    {
        Vector2 position = GetRandomSpawnPosition();

        if (CanSpawn(position)) return position;

        return GetSpawnablePosition();

    }

    bool CanSpawn(Vector2 pos)
    {
        bool result = true;

        RaycastHit2D hit2D = Physics2D.Raycast(pos, Vector2.down, 0.01f, backgroundMask);

        if (hit2D)
        {
            result = false;
        }

        return result;
        
    }

    Vector2 GetRandomSpawnPosition()
    {
        if (useCameraBounds && gameCamera != null)
        {
            return GetRandomPositionInCameraView();
        }
        return GetRandomPositionInFixedArea();
    }

    Vector2 GetRandomPositionInCameraView()
    {
        float cameraHeight = 2f * gameCamera.orthographicSize;
        float cameraWidth = cameraHeight * gameCamera.aspect;

        return new Vector2(
            Random.Range(gameCamera.transform.position.x - cameraWidth/2, gameCamera.transform.position.x + cameraWidth/2),
            Random.Range(gameCamera.transform.position.y - cameraHeight/2, gameCamera.transform.position.y + cameraHeight/2)
        );
    }

    Vector2 GetRandomPositionInFixedArea()
    {
        Vector2 center = transform.position;
        return new Vector2(
            Random.Range(center.x - spawnAreaSize.x/2, center.x + spawnAreaSize.x/2),
            Random.Range(center.y - spawnAreaSize.y/2, center.y + spawnAreaSize.y/2)
        );
    }

    // Visualize spawn area in Scene view
    void OnDrawGizmosSelected()
    {
        if (!useCameraBounds)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, spawnAreaSize);
        }
    }
}