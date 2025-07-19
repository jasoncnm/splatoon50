using UnityEngine;

public class MeleeEnemy : EnemyAbstract
{   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.OnPlayerHit(1.0f); // will use assets data
            Destroy(gameObject);
        }
    }
}