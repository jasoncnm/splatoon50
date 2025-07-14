using MoreMountains.Tools;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Normal, Rollling,
    }

    [SerializeField] float moveSpeed = 15f, acceleration = 10f;

    Transform gunEndPointTr;

    PlayerGunController gunController;

    [SerializeField] LayerMask splatterMask;

    public PlayerState state { get; private set; }
    
    Rigidbody2D rb2D;

    Vector2 contactNormal;

    Vector2 rollDir;

    Vector2 moveDir;

    Vector2 lastMoveDir;

    Animator playerAnimator;

    public Transform aim { get; private set; }

    public event EventHandler<OnShootEventArgs> shoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPos;
        public Vector3 shootPos;
        public Vector3 shootDir;
    }

    float turnSmoothTime = 0.01f;
    float nextShootTime = 0f;
    float fireRate;
    float rollSpeed;
    float triggerTimer = 0f;
    float fireDelay;

    bool _Shooting = false;

    public Vector3 positionBeforeDash { get; private set; }


    public void MoveSetup(Vector2 direction)
    {
        moveDir = direction;
        if (direction.x != 0 || direction.y != 0)
        {
            lastMoveDir = moveDir;
        }
    }

    public void OnShootStart()
    {
        triggerTimer = 0;
        _Shooting = true;
    }


    public void OnShootEnd()
    {
        _Shooting = false;
    }

    public void DashSetup()
    {
        if (state == PlayerState.Normal)
        {
            rollDir = lastMoveDir;
            rollSpeed = 20f;
            state = PlayerState.Rollling;

            playerAnimator.SetTrigger("Dash");
        }
    }

    private void Start()
    {

        state = PlayerState.Normal;
        playerAnimator = transform.Find("GFX").GetComponent<Animator>();

        positionBeforeDash = transform.position;
        aim = transform.Find("Aim");
        rb2D = transform.GetComponent<Rigidbody2D>();

        gunController = GetComponent<PlayerGunController>();
        GunSetUp();

    }

    public void GunSetUp()
    {
        fireRate = gunController.gunProperties.fireRate;
        fireDelay = gunController.gunProperties.fireDelay;

        gunEndPointTr = gunController.gunTr.Find("GunEndPoint");
    }

    void FixedUpdateEnd()
    {
        contactNormal = Vector2.zero;
    }

    void FixedUpdate()
    {
        Move();

        FixedUpdateEnd();
    }

    void Update()
    {

        if (state == PlayerState.Rollling)
        {
            OnDash();
        }

        Rotate();

        if (_Shooting)
        {
            OnShoot();
        }
    }

    void Move()
    {
        switch (state)
        {
            case PlayerState.Normal:
                {

                    float dot = Vector2.Dot(moveDir, contactNormal);

                    Vector2 targetdir = moveDir;

                    if (moveDir.magnitude < 0.01f)
                    {
                        playerAnimator.SetBool("Move", false);
                    }
                    else
                    {
                        playerAnimator.SetBool("Move", true);
                    }


                    if (dot < 0)
                    {
                        targetdir -= dot * contactNormal;
                    }

                    targetdir = targetdir.normalized;

                    Vector2 targetVelocity = targetdir * moveSpeed;

                    rb2D.linearVelocity = targetVelocity;
                }
                break;

            case PlayerState.Rollling:
                rb2D.linearVelocity = rollDir * rollSpeed;
                break;
        }

    }

    void Rotate()
    {

        Vector3 screenMousePos = Input.mousePosition;

#if UNITY_EDITOR

        screenMousePos.x = Mathf.Clamp(screenMousePos.x, 1, Handles.GetMainGameViewSize().x - 2);
        screenMousePos.y = Mathf.Clamp(screenMousePos.y, 1, Handles.GetMainGameViewSize().y - 2);

#endif

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);



        Vector3 aimDir = (mousePos - aim.position).normalized;

        float turnVelo = 0f;

        float targetangle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(aim.eulerAngles.z, targetangle, ref turnVelo, turnSmoothTime);
        
        

        aim.eulerAngles = new Vector3(aim.eulerAngles.x, 0, angle);

        SetSpriteFlip(aimDir, rollDir);
    }

    void SetSpriteFlip(Vector2 aimDir, Vector2 rollDir)
    {

        Transform playerGFX = transform.Find("GFX");
        Transform gunGFX = gunController.gunTr.Find("GFX");

        aimDir = aimDir.normalized;
        rollDir = rollDir.normalized;


        Vector3 playerAngles = playerGFX.localEulerAngles;
        Vector3 gunAngles = gunGFX.localEulerAngles;

        switch (state)
        {
            case PlayerState.Normal:
                {
                    if (aimDir.x < 0)
                    {  
                        playerGFX.localRotation = Quaternion.Euler(playerAngles.x, 180f, playerAngles.z);

                    }
                    else
                    {
                        playerGFX.localRotation = Quaternion.Euler(playerAngles.x, 0, playerAngles.z);
                    }
                }
                break;

            case PlayerState.Rollling:
                {
                    if (rollDir.x < 0)
                    {

                        playerGFX.localRotation = Quaternion.Euler(playerAngles.x, 180f, playerAngles.z);
  
                    }
                    else
                    {
                        playerGFX.localRotation = Quaternion.Euler(playerAngles.x, 0, playerAngles.z);
                    }
                }
                break;
        }

        if (aimDir.x < 0)
        {
            gunGFX.localRotation = Quaternion.Euler(180f, 0, 0);
        }
        else
        {

            gunGFX.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnShoot()
    {
        triggerTimer += Time.deltaTime;

        if (triggerTimer > fireDelay && Time.time > nextShootTime)
        {

            Vector3 endPointPos = gunEndPointTr.position;
            Vector3 shootPos = Util.GetMouseWorldPosition();
            Vector3 shootDir = shootPos - aim.position;

            shootDir = new Vector3(shootDir.x, shootDir.y, 0);

            shoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPos = endPointPos,
                shootPos = shootPos,
                shootDir = shootDir
            });
            nextShootTime = Time.time + fireRate;
        }
    }



    void OnDash()
    {
        float rollSpeedDropMultiplier = 5f;
        rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

        float rollSpeedMinimum = 5f;


        if (rollSpeed < rollSpeedMinimum)
        {
            state = PlayerState.Normal;
        }
    }

    void EvaluateCollision(Collision2D collision)
    {
        contactNormal = collision.contacts[0].normal;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        contactNormal = Vector2.zero;
    }

}
