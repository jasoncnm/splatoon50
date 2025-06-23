using MoreMountains.Feedbacks;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] PlayerController playerController;

    [SerializeField] MMFeedbacks shootFeedback;

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
        shootFeedback?.PlayFeedbacks(args.gunEndPointPos); 
    }

}
