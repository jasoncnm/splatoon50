using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField] Vector2 offset;

    [SerializeField] float moveSpeed = 1f, detectRange;

    BulletSpawner spawner;
    GunProperties gunProperties;

    float fireRate;
    float nextShootTime = 0f;

    private void Start()
    {
        spawner = GetComponentInChildren<BulletSpawner>();
        gunProperties = GetComponentInChildren<GunProperties>();
        fireRate = gunProperties.fireRate;
    }


    private void OnDrawGizmos()
    {
        if (GameManager.instance != null)
        {
            Gizmos.DrawWireSphere(GameManager.instance.player.position, detectRange);
        }
    }

    private void Update()
    {
        FollowPlayer();

        Collider2D[] colliderArr = Physics2D.OverlapCircleAll(GameManager.instance.player.position, detectRange);

        Enemy target = null;
        float minDist = float.MaxValue;
        foreach (Collider2D collider in colliderArr)
        {
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                float dist = Vector3.Distance(GameManager.instance.player.position, collider.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    target = enemy;
                }
            }
        }

        if (target)
        {
            Shoot(target);
        }

    }

    void Shoot(Enemy target)
    {
        Vector3 targetPos = target.GetComponent<Collider2D>().bounds.center;

        Vector3 dir = targetPos - transform.position;

        if (Time.time > nextShootTime)
        {
            spawner.SpawnBulllet(transform.position, dir);
            nextShootTime = Time.time + fireRate;
        }
    }

    void FollowPlayer()
    {
        Vector2 targetPosition = (Vector2)GameManager.instance.player.position + offset;

        Vector2 moveDir = (targetPosition - (Vector2)transform.position).normalized;

        float distance = Vector2.Distance(targetPosition, (Vector2)(transform.position));

        if (distance > 0)
        {
            Vector2 newPos = (Vector2)transform.position + moveDir * distance * moveSpeed * Time.deltaTime;

            transform.position = newPos;

        }
    }

}
