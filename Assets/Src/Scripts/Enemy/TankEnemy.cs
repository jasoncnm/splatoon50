using UnityEngine;
using System.Collections;

public class TankEnemy : EnemyAbstract
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float chargeTime = 3f;
    [SerializeField] private int damage = 30;
    [SerializeField] private LayerMask damageLayers;

    [Header("Visual Settings")]
    [SerializeField] private GameObject circleIndicator;
    [SerializeField] private Color startColor = new Color(1, 0, 0, 0.1f);
    [SerializeField] private Color endColor = new Color(1, 0, 0, 0.8f);
    
    [Header("Audio Settings")]
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip explosionSound;
    
    private AudioSource audioSource;
    private SpriteRenderer circleRenderer;
    private bool isCharging = false;
    private float cooldownTimer;
    private float chargeTimer;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Set up circle indicator
        if (circleIndicator != null)
        {
            circleRenderer = circleIndicator.GetComponent<SpriteRenderer>();
            if (circleRenderer == null)
            {
                circleRenderer = circleIndicator.AddComponent<SpriteRenderer>();
            }
            
            // Scale circle to match attack range
            float scale = attackRange * 2; // Assuming 1 unit = 1 meter in sprite scale
            circleIndicator.transform.localScale = new Vector3(scale, scale, 1f);
            
            // Position circle above ground
            circleIndicator.transform.position = transform.position + Vector3.up * 0.1f;
            
            circleIndicator.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Circle indicator not assigned to TankEnemy!");
        }
        
        cooldownTimer = attackCooldown;
    }
    
    void Update()
    {
        if (!isCharging)
        {
            cooldownTimer -= Time.deltaTime;
            
            if (cooldownTimer <= 0)
            {
                StartCoroutine(ChargeAttack());
            }
        }
        else
        {
            chargeTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(chargeTimer / chargeTime);
            
            // Update circle color
            if (circleRenderer != null)
            {
                circleRenderer.color = Color.Lerp(startColor, endColor, progress);
            }
        }
    }
    
    IEnumerator ChargeAttack()
    {
        isCharging = true;
        chargeTimer = 0f;
        
        // Activate circle indicator
        if (circleIndicator != null)
        {
            circleIndicator.SetActive(true);
            circleRenderer.color = startColor;
        }
        
        // Play charge sound
        if (audioSource != null && chargeSound != null)
        {
            audioSource.PlayOneShot(chargeSound);
        }
        
        // Wait for charge time
        yield return new WaitForSeconds(chargeTime);
        
        // Execute attack
        Explode();
        
        // Reset
        isCharging = false;
        cooldownTimer = attackCooldown;
        
        // Hide circle indicator
        if (circleIndicator != null)
        {
            circleIndicator.SetActive(false);
        }
    }
    
    void Explode()
    {
        // Play explosion sound
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
        
        // Find all colliders in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, damageLayers);
        
        // Apply damage to all targets
        foreach (Collider col in hitColliders)
        {
            // Check if target can take damage
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
    
    // Optional: Draw attack range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

// Interface for damageable objects
public interface IDamageable
{
    void TakeDamage(int amount);
}