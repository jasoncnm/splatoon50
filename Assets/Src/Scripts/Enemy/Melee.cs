using UnityEngine;
using UnityEngine.UI;

public class Melee : Enemy
{
    [SerializeField] Slider healthBar;

    [SerializeField] float initHealth, speed;

    Animator animator;

    private void Awake()
    {
        maxHealth = initHealth;
        moveSpeed = speed;
        health = initHealth;
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
        {
            animator.SetBool("Die", true);
            healthBar.gameObject.SetActive(false);
        }

        healthBar.value = health / maxHealth;

    }

    public override void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().linearVelocity = (Vector3)direction.normalized * moveSpeed;

    }

    public void StartDie()
    {
        GetComponent<Collider2D>().enabled = false;
        moveSpeed = 0;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
