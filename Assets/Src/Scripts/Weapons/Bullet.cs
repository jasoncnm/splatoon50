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


   public void Setup(Vector3 shootDir, float damage, float pierce, float fallOffDistance)
    {
        animator = GetComponent<Animator>();
        this.shootDir = shootDir.normalized;
        orgPos = transform.position;
        this.damage = damage;
        this.fallOffDistance = fallOffDistance;
        this.pierce = pierce;
    }

    private void Update()
    {
        transform.position += shootDir * Time.deltaTime * moveSpeed;
    }

    

    public void AfterHit()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        else if (Vector3.Distance(transform.position, orgPos) > transform.localScale.x)
        {
            moveSpeed = 0;
            animator.SetBool("Hit", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Vector3.Distance(transform.position, orgPos) > transform.localScale.x)
        {
            moveSpeed = 0;
            animator.SetBool("Hit", true);
        }
    }

}
