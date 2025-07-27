using UnityEngine;
using TMPro;

public class PlayerHealthTesting : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        // ---------------- Testing hot-keys ----------------
        if (Input.GetKeyDown(KeyCode.O))
            TakeDamage(10);               // ↓ key → -10 HP

        if (Input.GetKeyDown(KeyCode.P))
            Heal(10);                     // ↑ key → +10 HP
        // --------------------------------------------------
    }

    // IDamageable
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        RefreshUI();

        if (currentHealth <= 0)
            Die();
    }

    // Simple heal helper (clamped)
    private void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (healthText != null)
            healthText.text = $"HP: {currentHealth}/{maxHealth}";
    }

    private void Die()
    {
        // TODO: death / respawn logic
    }
}
