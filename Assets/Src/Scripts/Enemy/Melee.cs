using UnityEngine;
using UnityEngine.UI;

public class Melee : Enemy
{
    [SerializeField] Slider healthBar;

    [SerializeField] float initHealth, speed;

    private void Awake()
    {
        maxHealth = initHealth;
        moveSpeed = speed;
        health = initHealth;
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        // NOTE: Debug Only
        if (health == 0) health = maxHealth;


        healthBar.value = health / maxHealth;

    }

}
