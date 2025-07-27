using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MeleeEnemy : EnemyAbstract
{
    [SerializeField] private float attackCooldown = 0.5f;
    private float _lastAttackTime = -Mathf.Infinity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"MeleeEnemy collided with {collision.gameObject.name}");
        TryDamage(collision.gameObject);
    }

    private void TryDamage(GameObject targetGO)
    {
        if (Time.time - _lastAttackTime < attackCooldown) return;

        if (targetGO.TryGetComponent<IDamageable>(out var victim))
        {
            Debug.Log($"Damaging {targetGO.name} for {stats.Damage}");
            victim.TakeDamage(stats.Damage);
            _lastAttackTime = Time.time;
        }
        else
        {
            Debug.Log($"{targetGO.name} has no IDamageable");
        }
    }
}
