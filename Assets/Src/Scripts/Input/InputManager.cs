using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    PlayerGunController gunController;
    PlayerController playerController;
    CameraController camController;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        camController = Camera.main.GetComponent<CameraController>();
        gunController = playerController.GetComponent<PlayerGunController>();
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

        { // Player Reload
            if (Input.GetKeyDown(KeyCode.R))
            {
                gunController.Reload();
            }
        }


        //{ // Start Next Wave
        //    if ((GameManager.gameState == GameState.GAME_COMBAT_END) && Input.GetKeyDown(KeyCode.N))
        //    {
        //        GameManager.instance.SwitchState();
        //    }
        //}
    }

    private void LateUpdate()
    {
        camController.SetCameraPosition(playerController.transform.position);
    }

}
