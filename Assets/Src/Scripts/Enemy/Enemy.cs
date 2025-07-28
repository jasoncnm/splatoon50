using MoreMountains.Tools;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]

    [SerializeField] Slider healthBar;

    [SerializeField] protected float initHealth, moveSpeed;

    [Header("Damage Flash Effects")]

    [ColorUsage(true, true)]
    [SerializeField] Color flashColor = Color.white;
    [SerializeField, Range(0f, 1f)] float flashTime = 0.25f;
    [SerializeField] AnimationCurve flashCurve;


    float health, maxHealth;

    protected float currentSpeed;

    protected SpriteRenderer spriteRenderer;

    protected Material material;

    protected Animator animator;


    //protected Color flashColor = Color.white;
    //protected float flashTime = 0.25f;
    //protected AnimationCurve flashCurve;



    public void Stun()
    {
        StartCoroutine(StunCoroutine(0f));
    }

    IEnumerator StunCoroutine(float duration)
    {
        currentSpeed = 0f;
        yield return new WaitForSeconds(duration);
        currentSpeed = moveSpeed;
    }

    public abstract void TakeDamage(float damage, ElementalDamage elementalDamage);


    public virtual void Start()
    {
        currentSpeed = moveSpeed;
        maxHealth = initHealth;
        health = initHealth;
    }


    public virtual void Die()
    {
        Destroy(gameObject);
    }



    public virtual void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().linearVelocity = (Vector3)direction.normalized * currentSpeed;
    }

    protected void Damage(float damage)
    {

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
        {
            animator.SetBool("Die", true);
            
            currentSpeed = 0;
            GetComponent<Collider2D>().enabled = false;
            healthBar.gameObject.SetActive(false);

            DropCoins(1);
        }

        healthBar.value = health / maxHealth;

        StartCoroutine(DamageFlash());
    }


    IEnumerator DamageFlash()
    {

        material.SetColor("_FlashColor", flashColor);

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1, flashCurve.Evaluate(elapsedTime), (elapsedTime / flashTime));

            material.SetFloat("_FlashAmount", currentFlashAmount);

            yield return null;
        }
    }

    void DropCoins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * 0.5f;
            Transform drop = Instantiate(GameManager.instance.coin);    
            drop.position = transform.position + offset;
        }
    }

}
