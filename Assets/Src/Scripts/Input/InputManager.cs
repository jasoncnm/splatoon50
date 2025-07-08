using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public InputReader input;


    PlayerController playerController;
    CameraController camController;


    private void OnEnable()
    {
        input.moveEvent += OnMove;
        input.shootEvent += OnShoot;
        input.shootCancelledEvent += OnShootCancel;
        input.dashEvent += OnDash;
        input.dashCancelledEvent += OnDashCancel;
    }

    private void OnDisable()
    {
        input.moveEvent -= OnMove;
        input.shootEvent -= OnShoot;
        input.shootCancelledEvent -= OnShootCancel;
        input.dashEvent -= OnDash;
        input.dashCancelledEvent -= OnDashCancel;
    }

    void OnDash()
    {
        playerController.DashSetup();
    }

    void OnDashCancel()
    {
    }

    void OnShoot()
    {
        playerController.OnShootStart();
    }

    void OnShootCancel()
    {
        playerController.OnShootEnd();
    }

    void OnMove(Vector2 movement)
    {
        playerController.MoveSetup(movement);
    }

    private void Start()
    {
        playerController = GameManager.instance.player.GetComponent<PlayerController>();
        camController = Camera.main.GetComponent<CameraController>();
    }


    private void LateUpdate()
    {
        camController.SetCameraPosition(playerController.transform.position);
    }

}
