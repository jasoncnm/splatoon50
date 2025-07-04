using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;
public class PlayerGunController : MonoBehaviour
{

    PlayerController playerController;

    MMF_Player playerShootFeedback;

    BulletSpawner bulletSpawner;
    
    private void Start()
    {
        bulletSpawner = transform.Find("Aim").GetComponentInChildren<BulletSpawner>();
    }


    private void OnEnable()
    {
        playerController = GetComponent<PlayerController>();
        playerShootFeedback = transform.GetComponentInChildren<MMF_Player>();
        playerController.shoot += OnPlayerShoot;
    }

    private void OnDisable()
    {
        playerController.shoot -= OnPlayerShoot;
    }


    void OnPlayerShoot(object sender, PlayerController.OnShootEventArgs args)
    {
        playerShootFeedback?.PlayFeedbacks(args.gunEndPointPos);
        bulletSpawner.SpawnBulllet(args.gunEndPointPos, args.shootDir);

    }
}
