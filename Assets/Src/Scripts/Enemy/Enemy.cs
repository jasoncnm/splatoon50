using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float health, maxHealth, moveSpeed;

    public abstract void TakeDamage(float damage);

}
