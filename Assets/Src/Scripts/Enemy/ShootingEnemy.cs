using MoreMountains.Feedbacks;
using UnityEngine;

public class ShootingEnemy : Enemy
{

    [SerializeField, Range(0, 2)] float fireRate = 1f;

    float nextShootTime = 0;

    MMF_Player shootFeedback;

    private void Start()
    {
        shootFeedback = GetComponentInChildren<MMF_Player>();
    }

    void ShootPlayer()
    {
        shootFeedback?.PlayFeedbacks();
    }

    private void Update()
    {
        if (!GetComponent<EnemyNavigator>().isChasing)
        {
            if (Time.time > nextShootTime)
            {
                ShootPlayer();
                nextShootTime = Time.time + fireRate;
            }
        }
    }

}
