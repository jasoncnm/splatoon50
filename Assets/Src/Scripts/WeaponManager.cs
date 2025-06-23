using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;
public class WeaponManager : MonoBehaviour
{

    PlayerController playerController;

    MMF_Player shootFeedback;


    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        shootFeedback = playerController.transform.GetComponentInChildren<MMF_Player>();
    }

    private void OnEnable()
    {
        playerController.shoot += OnPlayerShoot;
    }

    private void OnDisable()
    {
        playerController.shoot -= OnPlayerShoot;
    }


    void OnPlayerShoot(object sender, PlayerController.OnShootEventArgs args)
    {
        //Debug.DrawLine(args.gunEndPointPos, args.shootPos, Color.white, .1f);
        shootFeedback?.PlayFeedbacks(args.gunEndPointPos);

    }

    public void SetBullet(ParticleSystem system)
    {
        MMF_Particles mmf_p = shootFeedback.GetFeedbackOfType<MMF_Particles>();

        if (mmf_p != null)
        {
            mmf_p.BoundParticleSystem = system;
        }

    }


}
