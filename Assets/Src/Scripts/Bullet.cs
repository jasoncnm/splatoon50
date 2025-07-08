using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Animator animator;

    [SerializeField, Range(0, 100)] float moveSpeed = 30f;

    Vector3 shootDir;

    Vector3 orgPos;

   public void Setup(Vector3 shootDir)
    {
        animator = GetComponent<Animator>();
        this.shootDir = shootDir.normalized;
        orgPos = transform.position;
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
            transform.position += 0.25f * shootDir;
            float dist = Vector3.Distance(collision.transform.position, orgPos);
            float damage = 20f;
            if (dist > 6.5f) damage = 10f;
            else if (dist > 15f) damage = 5f;

            Debug.Log(dist);

            collision.GetComponent<Enemy>().TakeDamage(damage);

        }

        moveSpeed = 0;
        animator.SetBool("Hit", true);
        
    }

}
