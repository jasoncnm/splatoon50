using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    PlayerController playerController;
    CameraController camController;

    private void Start()
    {
        playerController = GameManager.instance.player.GetComponent<PlayerController>();
        camController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        { // Player Movement Input
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            playerController.MoveSetup(new Vector2(h, v).normalized);
        }

        { // Player Fire Input
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                playerController.OnShootStart();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                playerController.OnShootEnd();
            }
        }

        { // Player Roll
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerController.DashSetup();
            }
        }
    }

    private void LateUpdate()
    {
        camController.SetCameraPosition(playerController.transform.position);
    }

}
