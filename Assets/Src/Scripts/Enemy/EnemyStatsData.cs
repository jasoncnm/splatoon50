using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Stats", fileName = "NewEnemyStats")]
public class EnemyStatsData : ScriptableObject
{
    [Header("Basic Stats")]
    public int MaxHealth = 100;
    public float MoveSpeed = 5f;
    public int AttackDamage = 10;
    
    [Header("Optional Special Stats")]
    public float AttackRange = 2f;
    public float AttackCooldown = 1f;
}