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

    Transform gunEndPointTr;

    PlayerGunController gunController;

    
    Rigidbody2D rb2D;

    Vector2 contactNormal;

    Vector2 rollDir;

    Vector2 moveDir;

    Vector2 lastMoveDir;

    Animator playerAnimator;

    float turnSmoothTime = 0.01f;
    float nextShootTime = 0f;
    float fireRate;
    float rollSpeed;
    float triggerTimer = 0f;
    float fireDelay;
    float dashDelay = 1f;
    float nextDashTime = 0f;

    [SerializeField] float moveSpeed = 15f, acceleration = 10f;

    [SerializeField] Transform interactIcon;

    public PlayerState state { get; private set; }
    public Transform aim { get; private set; }

    public Vector3 positionBeforeDash { get; private set; }

    public event EventHandler<OnShootEventArgs> shoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPos;
        public Vector3 shootPos;
        public Vector3 shootDir;
    }
    bool _Shooting = false;



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

        if (state == PlayerState.Normal && Time.time > nextDashTime)
        {
            rollDir = lastMoveDir;
            rollSpeed = 20f;
            state = PlayerState.Rollling;

            playerAnimator.SetTrigger("Dash");

            nextDashTime = Time.time + dashDelay;
        }
    }

    private void Start()
    {

        interactIcon.gameObject.SetActive(false);

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
        if (GameManager.gameIsPause) return;

        Move();

        FixedUpdateEnd();
    }

    void Update()
    {
        if (GameManager.gameIsPause) return;


        if (state == PlayerState.Rollling)
        {
            OnDash();
        }

        Rotate();

        if (_Shooting)
        {
            OnShoot();
        }

        InteractDetect();

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position, 2.0f);
        }
    }

    void InteractDetect()
    {
        float detectRange = 2.0f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectRange);

        Interactable item = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<Interactable>(out Interactable outItem))
            {
                if (item == null || 
                   (Vector3.Distance(transform.position, outItem.transform.position) < Vector3.Distance(transform.position, item.transform.position)))
                {
                    item = outItem;
                }
            }
        }

        if (item != null)
        {
            item.ReadyToInteract(interactIcon);

            if (Input.GetKeyDown(KeyCode.E))
            {
                item.Interact();
            }
        }
        else
        {
            interactIcon.gameObject.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PistolUpgrade"))
        {
            // Do something with game object
            // Destroy(collision.gameObject);
        }

    }

}
