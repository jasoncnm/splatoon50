using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public InputReader input;


    PlayerController playerController;
    CameraController camController;

    Vector2 moveDirection;

    public bool _DashHolding { get; private set; } = false;
    public bool _ShootHolding { get; private set; } = false;

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
        _DashHolding = true;
    }

    void OnDashCancel()
    {
        playerController.OnExitDash();
        _DashHolding = false;
    }

    void OnShoot()
    {
        _ShootHolding = true;
    }

    void OnShootCancel()
    {
        _ShootHolding = false;
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


        playerController.Rotate();


        if (_DashHolding)
        {
            playerController.OnDash();
        }

        if (_ShootHolding)
        {
            playerController.OnShoot();
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


        FixedUpdateEnd();
    }

    private void LateUpdate()
    {
        camController.SetCameraPosition(playerController.transform.position);
    }

}
