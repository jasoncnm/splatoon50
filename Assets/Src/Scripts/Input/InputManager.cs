using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputReader input;

    PlayerController playerController;
    CameraController camController;

    Vector2 moveDirection;

    public bool _DashHolding = false;

    private void OnEnable()
    {
        input.moveEvent += OnMove;
        input.shootEvent += OnShoot;
        input.dashEvent += OnDash;
        input.dashCancelledEvent += OnDashCancel;
    }

    private void OnDisable()
    {
        input.moveEvent -= OnMove;
        input.shootEvent -= OnShoot;
        input.dashEvent -= OnDash;
        input.dashCancelledEvent -= OnDashCancel;
    }

    void OnDash()
    {
        _DashHolding = true;
    }

    void OnDashCancel()
    {
        playerController.OnExitDash();
        _DashHolding = false;
    }

    void OnShoot()
    {
        playerController.OnShoot();
    }

    void OnMove(Vector2 movement)
    {
        moveDirection = movement;
    }

    private void Start()
    {
        playerController = GameManager.instance.player.GetComponent<PlayerController>();
        camController = Camera.main.GetComponent<CameraController>();
    }


    private void Update()
    {
        if (_DashHolding)
        {
            playerController.OnDash();
        }
    }

    void FixedUpdateEnd()
    {

    }

    void FixedUpdateStart()
    {

    }

    private void FixedUpdate()
    {
        FixedUpdateStart();

        playerController.Move(moveDirection);
        playerController.Rotate();

        FixedUpdateEnd();
    }

    private void LateUpdate()
    {
        camController.SetCameraPosition(playerController.transform.position);
    }

}
