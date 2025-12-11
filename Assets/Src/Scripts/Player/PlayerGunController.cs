using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
public class PlayerGunController : MonoBehaviour
{

    public UnityEvent shootEvent;
    public UnityEvent setGunEvent;
    public UnityEvent reloadStartEvent;
    public UnityEvent reloadStopEvent;

    PlayerController playerController = null;

    MMF_Player playerShootFeedback;

    Transform gunTr = null;

    public BulletSpawner bulletSpawner { get; private set; }
    public GunProperties gunProperties { get; private set; }

    public int bulletLeft { get; private set; }
    public int maxBullet { get; private set; }

    public float reloadTime { get; private set; }

    public bool reloading { get; private set; } = false;

    private void Start()
    {
        Transform aim = transform.Find("Aim");

        if (playerController == null) playerController = GetComponent<PlayerController>();

        for (int i = 0; i < aim.childCount; i++)
        {
            aim.GetChild(i).gameObject.SetActive(false);
        }

  
        SetGun(GameManager.startGunName);
        
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
        if (bulletLeft > 0 && !reloading)
        {
            playerShootFeedback?.PlayFeedbacks(args.gunEndPointPos);
            bulletSpawner.SpawnBulllet(args.gunEndPointPos, args.shootDir);
            bulletLeft--;
            shootEvent?.Invoke();
        }
        
        if (bulletLeft == 0)
        {
            Reload();
        }
    }

    public void SetGun(string name)
    {

        if (gunTr) gunTr.gameObject.SetActive(false);

        gunTr = transform.Find("Aim").Find(name);

        gunTr.gameObject.SetActive(true);

        bulletSpawner = gunTr.GetComponent<BulletSpawner>();
        gunProperties = gunTr.GetComponent<GunProperties>();

        playerController.GunSetUp();

        bulletLeft = maxBullet = gunProperties.bulletCapacity;

        reloadTime = gunProperties.reloadTime;

        setGunEvent?.Invoke();
    }

    IEnumerator Reloading()
    {
        reloading = true;

        reloadStartEvent?.Invoke();

        yield return new WaitForSeconds(reloadTime);

        bulletLeft = maxBullet;

        reloadStopEvent?.Invoke();
        reloading = false;

    }

    public void Reload()
    {
        if (!reloading && (bulletLeft < maxBullet)) StartCoroutine(Reloading());
    }

    public void UpgradeDamage()
    {
        gunTr.GetComponent<GunProperties>().damage += 5;
    }

    public void UpgradeCrits()
    {
        gunTr.GetComponent<GunProperties>().crits += 0.1f;
        if (gunTr.GetComponent<GunProperties>().crits > 100) gunTr.GetComponent<GunProperties>().crits = 100;
    }

    public void UpgradePierce()
    {
        gunTr.GetComponent<GunProperties>().pierce++;
    }

    public void UpgradeFirerate()
    {
        ref float firerate = ref gunTr.GetComponent<GunProperties>().fireRate;
        if (firerate > 0.1f) firerate -= 0.01f;
    }


    public Transform GunTr()
    {
        return gunTr;
    }

}
