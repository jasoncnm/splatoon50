using UnityEngine;

public class AssasinEnemy : EnemyAbstract
{    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.OnPlayerHit(0.1f);
            Destroy(gameObject);
        }
    }
}