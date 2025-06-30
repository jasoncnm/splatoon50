using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;
public class PlayerGunController : MonoBehaviour
{

    PlayerController playerController;

    MMF_Player playerShootFeedback;

    [SerializeField] ParticleSystem bulletParticles = null;


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
        if (bulletParticles != null)
        {
            SplatterSpawner.instance.Spawn();
            playerShootFeedback?.PlayFeedbacks(args.gunEndPointPos);
        }

    }



    public void SetBullet(ParticleSystem system)
    {
        MMF_Particles mmf_p = playerShootFeedback.GetFeedbackOfType<MMF_Particles>();

        if (mmf_p != null)
        {
            bulletParticles = system;
            mmf_p.BoundParticleSystem = system;
        }

    }


}
