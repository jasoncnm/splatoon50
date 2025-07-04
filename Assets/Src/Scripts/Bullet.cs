using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Animator animator;

    [SerializeField, Range(0, 100)] float moveSpeed = 30f;

    Vector3 shootDir;

    bool hit = false;
   public void Setup(Vector3 shootDir)
    {
        animator = GetComponent<Animator>();
        this.shootDir = shootDir.normalized;
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

        Debug.Log(collision.name);

        moveSpeed = 0;
        animator.SetBool("Hit", true);
        
    }

}
