using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    [Header("Enemy Configuration")]
    [SerializeField] protected EnemyStatsData stats;
    
    private int currentHealth;
    
    protected virtual void Awake()
    {
        currentHealth = stats.MaxHealth;
    }
    
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0) Die();
    }
    
    protected virtual void Die()
    {
        // Common death logic
        Destroy(gameObject);
    }
    
    // Public properties for other scripts to access
    public float MoveSpeed => stats.MoveSpeed;
    public int Damage => stats.AttackDamage;
    public int CurrentHealth => currentHealth;
}