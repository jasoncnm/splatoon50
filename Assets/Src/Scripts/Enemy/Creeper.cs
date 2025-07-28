using System.Collections;
using UnityEngine;

/// <summary>
/// Creeper explodes when the player is close enough.
/// Attach to the Creeper prefab (inherits base stats from Enemy).
/// </summary>
public class CreeperEnemy : EnemyAbstract
{
    [Header("Explosion Settings")]
    [Tooltip("Distance at which the creeper arms itself")]
    public float triggerRange = 1.5f;

    [Tooltip("Damage radius of the explosion")]
    public float explosionRadius = 2.5f;

    [Tooltip("Hit-points removed from the player")]
    public int damage = 40;

    [Tooltip("Seconds between arming and detonation")]
    public float fuseTime = 0.5f;

    [Tooltip("Prefab with explosion animation + (optional) sound")]
    public GameObject explosionPrefab;

    [Tooltip("Layers that get hurt (include Player)")]
    public LayerMask damageMask;

    private Transform player;
    private bool exploding;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (exploding) return;

        if (Vector2.Distance(transform.position, player.position) <= triggerRange)
            StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        exploding = true;

        // TODO: stop movement / play fuse effect (sssssssss)

        yield return new WaitForSeconds(fuseTime);

        // Spawn explosion visual + SFX
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Apply damage
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageMask);
        foreach (var h in hits)
        {
            if (h.CompareTag("Player"))
            {
                // TODO: hook into player health script
            }
        }

        Destroy(gameObject);
    }

    // scene-view helpers
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
