using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;
public class PlayerGunController : MonoBehaviour
{

    PlayerController playerController = null;

    MMF_Player playerShootFeedback;

    Transform gunTr = null;

    public BulletSpawner bulletSpawner { get; private set; }
    public GunProperties gunProperties { get; private set; }
    
  

    private void Start()
    {
        Transform aim = transform.Find("Aim");

        if (playerController == null) playerController = GetComponent<PlayerController>();

        for (int i = 0; i < aim.childCount; i++)
        {
            aim.GetChild(i).gameObject.SetActive(false);
        }

        if (gunTr == null)
        {
            SetGun(GameManager.startGunName);
        }
    }

    private void OnEnable()
    {
        if (playerController == null) playerController = GetComponent<PlayerController>();
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

        if (gunTr) gunTr.gameObject.SetActive(false);

        gunTr = transform.Find("Aim").Find(name);

        bulletSpawner = gunTr.GetComponent<BulletSpawner>();
        gunProperties = gunTr.GetComponent<GunProperties>();

        playerController.GunSetUp();

        gunTr.gameObject.SetActive(true);

    }

    public Transform GunTr()
    {
        return gunTr;
    }

}
