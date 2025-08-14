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

    public int attackPower;

    [Header("Damage Flash Effects")]

    [ColorUsage(true, true)]
    [SerializeField] Color flashColor = Color.white;
    [SerializeField, Range(0f, 1f)] float flashTime = 0.25f;
    [SerializeField] AnimationCurve flashCurve;


    float health, maxHealth;

    bool effectStart = false;
    bool effectEnd = false;

    protected GameManager gm;

    protected float currentSpeed;

    protected SpriteRenderer spriteRenderer;

    protected Material material;

    protected Animator animator;


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

    public virtual void TakeDamage(float damage, ElementalDamage elementalDamage)
    {
        Damage(damage);

        switch (elementalDamage)
        {
            case ElementalDamage.FIRE:
                {
                    float fireDuration = 5f;
                    float fireDamage = 10f;
                    if (!effectStart)
                    {
                        material.SetColor("_BlendColor", Color.red);
                        StartCoroutine(LastingDamage(fireDuration, fireDamage));

                        if (effectEnd)
                        {

                            material.SetFloat("_BlendAmount", 0f);
                        }    
                    }
                    break;
                }
            case ElementalDamage.LIGHTING:
                {

                    if (!transform.GetComponentInChildren<LightingStruck>()) // && !transform.GetComponentInChildren<ChainLighting>())
                    {
                        Instantiate(gm.beenStruck, transform);
                        GameObject obj = Instantiate(gm.chainLightingEffect, transform.position, Quaternion.identity);
                        obj.transform.parent = transform;

                        ChainLighting.damage = 10;
                        ChainLighting.amountToChain = 5;

                    }

                    break;
                }

            case ElementalDamage.ICE:
                {
                    if (!effectStart)
                    {
                        material.SetColor("_BlendColor", Color.blue);
                        StartCoroutine(SlowDown(5f, 0.5f));


                        if (effectEnd) material.SetFloat("_BlendAmount", 0f);

                    }
                    break;
                }

            case ElementalDamage.POSION:
                {
                    if (!effectStart)
                    {
                        material.SetColor("_BlendColor", new Color(0.7f, 0.1f, 0.1f));
                       float duration = 5f;
                        float _damage = 5f;
                        float speedRate = 0.7f;

                        StartCoroutine(SlowDown(duration, speedRate));
                        StartCoroutine(LastingDamage(duration, _damage));


                    }

                    break;
                }
        }
    }


    public virtual void Start()
    {

        gm = GameManager.instance;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

        currentSpeed = moveSpeed;
        maxHealth = initHealth;
        health = initHealth;
    }


    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void StartDie()
    {
        animator.SetBool("Die", true);
        GetComponent<Collider2D>().enabled = false;
        healthBar.gameObject.SetActive(false);

        currentSpeed = 0;
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
            StartDie();
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

    IEnumerator SlowDown(float duration, float slowRate)
    {
        material.SetFloat("_BlendAmount", 0.5f);

        effectStart = true;

        currentSpeed *= slowRate;

        yield return new WaitForSeconds(duration);

        currentSpeed = moveSpeed;

        effectStart = false;

        material.SetFloat("_BlendAmount", 0f);
    }

    IEnumerator LastingDamage(float duration, float damage)
    {
        material.SetFloat("_BlendAmount", 0.5f);

        effectStart = true;

        float timer = 0;

        for (;timer < duration;)
        {
            yield return new WaitForSeconds(1f);

            Damage(damage);
            timer++;
        }

        effectStart = false;
        material.SetFloat("_BlendAmount", 0f);
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
