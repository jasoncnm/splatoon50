using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/Input Reader", fileName = "Input Reader")]
public class InputReader : ScriptableObject
{
    
    [SerializeField] public InputActionAsset asset;

    public event UnityAction<Vector2> moveEvent;
    public event UnityAction<Vector2> aimEvent;

    public event UnityAction shootEvent;
    public event UnityAction shootCancelledEvent;

    public event UnityAction pauseEvent;
    public event UnityAction unPauseEvent;

    public event UnityAction interactEvent;
    public event UnityAction interactCancelledEvent;

    public event UnityAction dashEvent;
    public event UnityAction dashCancelledEvent;

    InputAction shootAction;
    InputAction aimAction;
    InputAction moveAction;
    InputAction pauseAction;
    InputAction unPauseAction;
    InputAction interactAction;
    InputAction dashAction;
    
    private void OnEnable()
    {
        aimAction = asset.FindAction("Aim", true);
        moveAction = asset.FindAction("Move", true);
        pauseAction = asset.FindAction("Pause", true);
        unPauseAction = asset.FindAction("UnPause", true);
        interactAction = asset.FindAction("Interact", true);
        shootAction = asset.FindAction("Shoot", true);
        dashAction = asset.FindAction("Dash", true);

        dashAction.started += OnDash;
        dashAction.performed += OnDash;
        dashAction.canceled += OnDash;

        shootAction.started += OnShoot;
        shootAction.performed += OnShoot;
        shootAction.canceled += OnShoot;

        aimAction.started += Onaim;
        aimAction.performed += Onaim;
        aimAction.canceled += Onaim;

        moveAction.started   += OnMove;
        moveAction.performed += OnMove;
        moveAction.canceled  += OnMove;

        pauseAction.started   += OnPause;
        pauseAction.performed += OnPause;
        pauseAction.canceled  += OnPause;

        unPauseAction.started += OnUnPause;
        unPauseAction.performed += OnUnPause;
        unPauseAction.canceled += OnUnPause;

        interactAction.started += OnInteract;
        interactAction.performed += OnInteract;
        interactAction.canceled += OnInteract;

        dashAction.Enable();
        shootAction.Enable();
        aimAction.Enable();
        moveAction.Enable();
        pauseAction.Enable();
        unPauseAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        aimAction.started -= Onaim;
        aimAction.performed -= Onaim;
        aimAction.canceled -= Onaim;

        dashAction.started -= OnDash;
        dashAction.performed -= OnDash;
        dashAction.canceled -= OnDash;

        moveAction.started -= OnMove;
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        pauseAction.started -= OnPause;
        pauseAction.performed -= OnPause;
        pauseAction.canceled -= OnPause;

        unPauseAction.started -= OnUnPause;
        unPauseAction.performed -= OnUnPause;
        unPauseAction.canceled -= OnUnPause;

        interactAction.started -= OnInteract;
        interactAction.performed -= OnInteract;
        interactAction.canceled -= OnInteract;

        shootAction.started -= OnShoot;
        shootAction.performed -= OnShoot;
        shootAction.canceled -= OnShoot;

        dashAction.Disable();
        shootAction.Disable();
        aimAction.Disable();
        moveAction.Disable();
        pauseAction.Disable();
        unPauseAction.Disable();
        interactAction.Disable();
    }

    void OnDash(InputAction.CallbackContext context)
    {
        if (dashEvent != null && context.started)
        {
            dashEvent.Invoke();
        }

        if (dashCancelledEvent != null && context.canceled)
        {
            dashCancelledEvent.Invoke();
        }
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        if (shootEvent != null && context.started)
        {
            shootEvent.Invoke();
        }

        if (shootCancelledEvent != null && context.canceled)
        {
            shootCancelledEvent.Invoke();
        }
    }

    void OnUnPause(InputAction.CallbackContext context)
    {
        if (unPauseEvent != null && context.started)
        {
            unPauseEvent.Invoke();
        }
    }

    void OnPause(InputAction.CallbackContext context)
    {
        if (pauseEvent != null && context.started)
        {
            pauseEvent.Invoke();
        }
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveEvent?.Invoke(context.ReadValue<Vector2>());

    }

    void Onaim(InputAction.CallbackContext  context)
    {
        aimEvent?.Invoke(context.ReadValue<Vector2>());
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        if (interactEvent != null && context.started)
        {
            interactEvent.Invoke();
        }

        if (interactCancelledEvent != null && context.canceled)
        {
            interactCancelledEvent.Invoke();
        }
    }

}
