using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WitchEnemy : EnemyAbstract
{
    [Header("Spawning Settings")]
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int maxSpawnCount = 3;
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    
    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem spawnEffect;
    [SerializeField] private float effectDuration = 1.5f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip spawnSound;
    
    private AudioSource audioSource;
    private float spawnTimer;
    private int currentSpawnCount;
    private bool isSpawning;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f; // 3D sound
        }
        
        spawnTimer = spawnInterval;
        currentSpawnCount = 0;
    }

    void Update()
    {
        if (currentSpawnCount >= maxSpawnCount) return;
        
        spawnTimer -= Time.deltaTime;
        
        if (spawnTimer <= 0 && !isSpawning)
        {
            StartCoroutine(SpawnEnemyRoutine());
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        isSpawning = true;
        
        // Choose a random enemy to spawn
        GameObject enemyToSpawn = GetRandomEnemyPrefab();
        if (enemyToSpawn == null) yield break;
        
        // Calculate spawn position
        Vector3 spawnPosition = GetSpawnPosition();
        
        // Play spawn effect
        PlaySpawnEffect(spawnPosition);
        
        // Play sound
        if (spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
        
        // Wait for effect to complete
        yield return new WaitForSeconds(effectDuration);
        
        // Actually spawn the enemy
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        
        // Update counters
        currentSpawnCount++;
        spawnTimer = spawnInterval;
        isSpawning = false;
    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned to Witch!");
            return null;
        }
        
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        return enemyPrefabs[randomIndex];
    }

    private Vector3 GetSpawnPosition()
    {
        // Get random point around witch
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        
        // Make sure it's on ground
        if (Physics.Raycast(spawnPosition + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f))
        {
            spawnPosition = hit.point + Vector3.up * 0.1f;
        }
        
        return spawnPosition;
    }

    private void PlaySpawnEffect(Vector3 position)
    {
        if (spawnEffect != null)
        {
            ParticleSystem effect = Instantiate(spawnEffect, position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + 1f);
        }
    }

    // Visualize spawn radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}