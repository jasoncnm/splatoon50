using UnityEngine;

public class CreeperEnemy : EnemyAbstract
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionTriggerRange = 3f;
    [SerializeField] private float explosionDamageRadius = 5f;
    [SerializeField] private int explosionDamage = 50;
    [SerializeField] private float explosionDelay = 1.5f;
    [SerializeField] private LayerMask damageLayers;
    
    [Header("Visual Effects")]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] private float pulseSpeed = 3f;
    [SerializeField] private float maxIndicatorScale = 1.2f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip warningSound;
    [SerializeField] private AudioClip explosionSound;
    
    private AudioSource audioSource;
    private Renderer indicatorRenderer;
    private Transform player;
    private bool isPrimed;
    private bool isExploding;
    private float explosionTimer;
    private Color originalColor;
    private Vector3 originalScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        
        // Set up range indicator
        if (rangeIndicator != null)
        {
            indicatorRenderer = rangeIndicator.GetComponent<Renderer>();
            originalColor = indicatorRenderer.material.color;
            originalScale = rangeIndicator.transform.localScale;
            
            // Scale indicator to match explosion radius
            float scale = explosionDamageRadius * 2f;
            rangeIndicator.transform.localScale = new Vector3(scale, 0.1f, scale);
        }
    }

    void Update()
    {
        if (isExploding || player == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // Check if player is in trigger range
        if (distanceToPlayer <= explosionTriggerRange && !isPrimed)
        {
            PrimeForExplosion();
        }
        
        // Handle explosion countdown
        if (isPrimed)
        {
            explosionTimer -= Time.deltaTime;
            
            // Update visual warning
            UpdateWarningEffect();
            
            if (explosionTimer <= 0)
            {
                Explode();
            }
        }
    }

    private void PrimeForExplosion()
    {
        isPrimed = true;
        explosionTimer = explosionDelay;
        
        // Play warning sound
        if (warningSound != null)
        {
            audioSource.PlayOneShot(warningSound);
        }
        
        // Optional: Stop movement if you have a movement component
        // GetComponent<EnemyMovement>().StopMoving();
    }

    private void UpdateWarningEffect()
    {
        if (rangeIndicator == null || indicatorRenderer == null) return;
        
        // Pulse the indicator
        float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        Color currentColor = Color.Lerp(originalColor, warningColor, pulse);
        indicatorRenderer.material.color = currentColor;
        
        // Scale pulse effect
        float scaleFactor = Mathf.Lerp(1f, maxIndicatorScale, pulse);
        rangeIndicator.transform.localScale = originalScale * scaleFactor;
    }

    private void Explode()
    {
        isExploding = true;
        
        // Play explosion sound
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
        
        // Create explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        
        // Apply damage
        ApplyExplosionDamage();
        
        // Destroy the creeper
        Destroy(gameObject);
    }

    private void ApplyExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionDamageRadius, damageLayers);
        
        foreach (Collider col in hitColliders)
        {
            // Apply damage with falloff based on distance
            float distance = Vector3.Distance(transform.position, col.transform.position);
            float damageMultiplier = 1 - Mathf.Clamp01(distance / explosionDamageRadius);
            int calculatedDamage = Mathf.RoundToInt(explosionDamage * damageMultiplier);
            
            // Apply damage to damageable objects
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(calculatedDamage);
            }
            
            // Optional: Apply force to rigidbodies
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 explosionDirection = (col.transform.position - transform.position).normalized;
                rb.AddForce(explosionDirection * (explosionDamage * 10f) * damageMultiplier, ForceMode.Impulse);
            }
        }
    }

    // Visualize ranges in editor
    void OnDrawGizmosSelected()
    {
        // Draw trigger range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionTriggerRange);
        
        // Draw damage radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionDamageRadius);
    }
}