using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int MaxHealth;
        public float Speed;
        public int Damage;
    }

    [SerializeField]
    protected EnemyStats stats;

    protected int currentHealth;

    protected virtual void Awake()
    {
        currentHealth = stats.MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        // Common death logic
        Destroy(gameObject);
    }

    // Optional: Public getter for stats if needed by child classes
    public EnemyStats GetStats()
    {
        return stats;
    }
}
