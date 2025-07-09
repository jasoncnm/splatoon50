using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;
public class PlayerGunController : MonoBehaviour
{

    PlayerController playerController;

    MMF_Player playerShootFeedback;

    public Transform gunTr { get; private set; }

    public BulletSpawner bulletSpawner { get; private set; }
    public GunProperties gunProperties { get; private set; }
    
    

    private void Awake()
    {
        Transform aim = transform.Find("Aim");

        for (int i = 0; i < aim.childCount; i++)
        {
           aim.GetChild(i).gameObject.SetActive(false);
        }

        gunTr = aim.Find("Gun_AR");

        bulletSpawner = gunTr.GetComponent<BulletSpawner>();
        gunProperties = gunTr.GetComponent<GunProperties>();

        gunTr.gameObject.SetActive(true);
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

    public void SetGun(string name)
    {

        gunTr.gameObject.SetActive(false);

        gunTr = transform.Find("Aim").Find(name);

        bulletSpawner = gunTr.GetComponent<BulletSpawner>();
        gunProperties = gunTr.GetComponent<GunProperties>();

        playerController.GunSetUp();

        gunTr.gameObject.SetActive(true);

    }

}
