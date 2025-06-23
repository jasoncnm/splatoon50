using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputReader input;

    PlayerController playerController;
    CameraController camController;

    Vector2 moveDirection;
    
    private void OnEnable()
    {
        input.moveEvent += OnMove;
        input.shootEvent += OnShoot;
    }

    private void OnDisable()
    {
        input.moveEvent -= OnMove;
        input.shootEvent -= OnShoot;
    }

    void OnShoot()
    {
        playerController.OnShoot();
    }

    void OnMove(Vector2 movement)
    {
        moveDirection = movement;
    }

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        camController = FindAnyObjectByType<CameraController>();
    }

    void FixedUpdateEnd()
    {

    }

    void FixedUpdateStart()
    {

    }

    private void Update()
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
