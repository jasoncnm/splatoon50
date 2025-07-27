using UnityEngine;

public class SniperEnemy : EnemyAbstract
{
    public Transform firePoint;  // The position where the laser starts
    public LineRenderer laserLine;
    public LayerMask visionMask; // Make sure this includes "Player" but excludes "Walls"
    public float timeBeforeShot = 3f; // Total aiming time before shooting
    public GameObject chargeEffectPrefab; // Shown 1 second before shot
    public AudioClip chargeSound;
    public AudioSource audioSource;

    private Transform player;
    private float aimTimer;
    private bool isAiming;
    private bool isCharging;
    private GameObject chargeEffectInstance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        laserLine.enabled = false;
    }

    // Update timng
    void Update()
    {
        if (CanSeePlayer())
        {
            if (!isAiming)
            {
                StartAiming();
            }

            AimLaser();
            aimTimer += Time.deltaTime;

            if (!isCharging && aimTimer >= timeBeforeShot - 1f)
            {
                TriggerChargeEffect();
            }

            if (aimTimer >= timeBeforeShot)
            {
                Shoot();
                ResetAiming();
            }
        }
        else if (isAiming)
        {
            ResetAiming();
        }
    }

    bool CanSeePlayer()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        float distance = Vector2.Distance(firePoint.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, distance, visionMask);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void StartAiming()
    {
        isAiming = true;
        aimTimer = 0f;
        isCharging = false;
        laserLine.enabled = true;
    }

    void AimLaser()
    {
        if (!laserLine.enabled) return;

        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, player.position);
    }

    void TriggerChargeEffect()
    {
        isCharging = true;
        if (chargeEffectPrefab != null)
        {
            chargeEffectInstance = Instantiate(chargeEffectPrefab, firePoint.position, Quaternion.identity);
        }
        if (chargeSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(chargeSound);
        }
    }

    void Shoot()
    {
        // TODO: Implement shooting logic
    }

    void ResetAiming()
    {
        isAiming = false;
        isCharging = false;
        aimTimer = 0f;
        laserLine.enabled = false;

        if (chargeEffectInstance != null)
        {
            Destroy(chargeEffectInstance);
        }
    }
}
