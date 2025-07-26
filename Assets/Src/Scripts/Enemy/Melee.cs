using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Melee : Enemy
{
    GameManager gm;

    bool fire = false;
    bool ice = false;

    //private void Start()
    //{
    //    animator = GetComponent<Animator>();
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    gm = GameManager.instance;

    //    maxHealth = initHealth;
    //    moveSpeed = speed;
    //    health = initHealth;
    //    material = spriteRenderer.material;
    //    healthBar = meleeHealthBar;

    //    base.flashColor = flashColor;
    //    base.flashTime = flashTime;
    //    base.flashCurve = flashCurve;

    //}

    public override void Start()
    {

        base.Start();

        gm = GameManager.instance;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }



    public override void TakeDamage(float damage, ElementalDamage elementalDamage)
    {
        Damage(damage);

        switch(elementalDamage)
        {
            case ElementalDamage.FIRE:
                {
                    float fireDuration = 5f;
                    float fireDamage = 10f;
                    if (!fire) StartCoroutine(TakeFireDamage(elementalDamage, fireDuration, fireDamage));
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
                    if (!ice) StartCoroutine(IceSlowDown(5f));
                    break;
                }
        }

    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        // GetComponent<Rigidbody2D>().linearVelocity = (Vector3)direction.normalized * moveSpeed;
    }


    public override void Die()
    {
        base.Die();
    }

    IEnumerator IceSlowDown(float duration)
    {
        ice = true;
        material.SetColor("_BlendColor", Color.blue);
        material.SetFloat("_BlendAmount", 0.5f);
        currentSpeed *= 0.5f;

        yield return new WaitForSeconds(duration);

        material.SetFloat("_BlendAmount", 0f);
        currentSpeed = moveSpeed;

        ice = false;
    }

    IEnumerator TakeFireDamage(ElementalDamage elementalDamage, float duration, float damage)
    {
        material.SetColor("_BlendColor", Color.red);
        material.SetFloat("_BlendAmount", 0.5f);
        fire = true;
        float timer = 0;
        for (;;)
        {
            Debug.Log("Timer: " + timer);
            yield return new WaitForSeconds(1f);

            Damage(damage);
            timer++;       
            
            if (timer > duration)
            {
                fire = false;
                break;
            }
        }
        material.SetFloat("_BlendAmount", 0f);
    }

}
