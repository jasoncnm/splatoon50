using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Animator animator;

    [SerializeField, Range(0, 100)] float moveSpeed = 30f;

    Vector3 shootDir;

    Vector3 orgPos;

    float damage, fallOffDistance, pierce;

    bool hit = false;


    public void Setup(Vector3 shootDir, float damage, float pierce, float fallOffDistance)
    {

        animator = GetComponent<Animator>();
        this.shootDir = shootDir.normalized;
        orgPos = transform.position;
        this.damage = damage;
        this.fallOffDistance = fallOffDistance;
        this.pierce = pierce;

        hit = false;
     
        Collider2D projCollider = GetComponent<Collider2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, projCollider.bounds.extents.x, projCollider.includeLayers);
        foreach (Collider2D collider in colliders)
        {
            EvaluateCollision(collider);
        }

    }
    private void Update()
    {
        GetComponent<Rigidbody2D>().linearVelocity = shootDir * moveSpeed;
    }

    

    public void AfterHit()
    {
        Destroy(gameObject);
    }

    void EvaluateCollision(Collider2D collision)
    {

        if (collision.CompareTag("Enemies"))
        {
            pierce--;

            float dist = Vector3.Distance(collision.transform.position, orgPos);

            if (dist > fallOffDistance) damage *= 0.5f;
            else if (dist > fallOffDistance * 2) damage *= 0.25f;
            collision.GetComponent<Enemy>().TakeDamage(damage);

            if (pierce < 0)
            {

                transform.position += 0.25f * shootDir;
                moveSpeed = 0;
                animator.SetBool("Hit", true);
            }
        }
        else
        {
            moveSpeed = 0;
            animator.SetBool("Hit", true);
        }

        hit = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hit) EvaluateCollision(collision);

        // moveSpeed = 0;
    }

}
