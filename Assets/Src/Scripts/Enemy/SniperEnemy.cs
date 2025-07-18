using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections;

public class SniperEnemy : EnemyAbstract
{
    [Header("Player Detection")]
    [SerializeField] private float detectionRange = 30f;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private LayerMask playerLayer;
    
    [Header("Aiming Settings")]
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private Color aimingColor = Color.red;
    [SerializeField] private float laserWidth = 0.05f;
    
    [Header("Attack Settings")]
    [SerializeField] private float shootDelay = 3f;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private AudioClip warningSound;
    
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource audioSource;

    private Transform player;
    private GameObject currentWarning;
    private bool isAiming;
    private bool isInWarningPhase;
    private float aimTimer;

    void Start()
    {
        // Initialize laser
        laserLine.startWidth = laserWidth;
        laserLine.endWidth = laserWidth;
        laserLine.enabled = false;
        
        // Find player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            if (!isAiming)
            {
                // Start aiming sequence
                StartAiming();
            }
            
            // Always update laser position while aiming
            UpdateLaserPosition();
            
            // Handle aiming sequence
            HandleAimingSequence();
        }
        else if (isAiming)
        {
            // Player lost, reset
            ResetAiming();
        }
    }

    private bool CanSeePlayer()
    {
        if (player == null) return false;
        
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        
        if (distanceToPlayer > detectionRange) return false;
        
        // Check for obstacles
        if (Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayers))
        {
            return false;
        }
        
        // Final check for player
        return Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, 
                              detectionRange, playerLayer);
    }

    private void StartAiming()
    {
        isAiming = true;
        aimTimer = 0f;
        laserLine.enabled = true;
        laserLine.startColor = aimingColor;
        laserLine.endColor = aimingColor;
    }

    private void UpdateLaserPosition()
    {
        if (laserLine == null || player == null) return;
        
        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, player.position);
    }

    private void HandleAimingSequence()
    {
        aimTimer += Time.deltaTime;
        
        // 1 second before shooting: show warning
        if (aimTimer >= shootDelay - 1f && !isInWarningPhase)
        {
            StartWarningPhase();
        }
        
        // Time to shoot
        if (aimTimer >= shootDelay)
        {
            Shoot();
            ResetAiming();
        }
    }

    private void StartWarningPhase()
    {
        isInWarningPhase = true;
        
        // Create warning indicator
        if (warningPrefab != null)
        {
            // Position at player's location
            currentWarning = Instantiate(warningPrefab, player.position + Vector3.up * 2f, 
                                       Quaternion.identity);
        }
        
        // Play warning sound
        if (audioSource != null && warningSound != null)
        {
            audioSource.PlayOneShot(warningSound);
        }
        
        // Visual feedback: change laser to blinking
        StartCoroutine(BlinkLaser());
    }

    private IEnumerator BlinkLaser()
    {
        while (isInWarningPhase)
        {
            laserLine.startColor = Color.yellow;
            laserLine.endColor = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            
            laserLine.startColor = Color.red;
            laserLine.endColor = Color.red;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Shoot()
    {
        // YOUR SHOOTING CODE GOES HERE
        // Example: Instantiate projectile, apply damage, etc.
        Debug.Log("Sniper Fired!");
    }

    private void ResetAiming()
    {
        isAiming = false;
        isInWarningPhase = false;
        aimTimer = 0f;
        laserLine.enabled = false;
        
        // Clean up warning
        if (currentWarning != null)
        {
            Destroy(currentWarning);
        }
        
        StopAllCoroutines();
    }
}