using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;
public class WeaponManager : MonoBehaviour
{

    PlayerController playerController;

    MMF_Player shootFeedback;

    ParticleSystem bulletParticles = null;

    private void OnEnable()
    {
        playerController = GameManager.instance.player.GetComponent<PlayerController>();
        shootFeedback = playerController.transform.GetComponentInChildren<MMF_Player>();
        playerController.shoot += OnPlayerShoot;
    }

    private void OnDisable()
    {
        playerController.shoot -= OnPlayerShoot;
    }


    void OnPlayerShoot(object sender, PlayerController.OnShootEventArgs args)
    {
        if (bulletParticles != null)
        {
            SplatterSpawner.instance.Spawn();
            shootFeedback?.PlayFeedbacks(args.gunEndPointPos);
        }

    }



    public void SetBullet(ParticleSystem system)
    {
        MMF_Particles mmf_p = shootFeedback.GetFeedbackOfType<MMF_Particles>();

        if (mmf_p != null)
        {
            bulletParticles = system;
            mmf_p.BoundParticleSystem = system;
        }

    }


}
