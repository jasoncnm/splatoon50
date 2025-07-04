using MoreMountains.Tools;
using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f, acceleration = 10f, fireRate = 0.3f;

    [SerializeField] Transform gunEndPointTr;

    [SerializeField] LayerMask splatterMask;
    
    Rigidbody2D rb2D;

    SpriteRenderer[] renderers;

    Vector2 contactNormal;

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


    public bool _Dashing { get; private set; } = false;


    public Vector3 positionBeforeDash { get; private set; }

    public void FixedUpdateEnd()
    {
        contactNormal = Vector2.zero;
    }


    private void Start()
    {
        playerAnimator = transform.Find("GFX").GetComponent<Animator>();
        renderers = GetComponentsInChildren<SpriteRenderer>();

        positionBeforeDash = transform.position;
        aim = transform.Find("Aim");
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {

        float dot = Vector2.Dot(direction, contactNormal);

        Vector2 targetdir = direction;

        if (direction.magnitude < 0.01f)
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

    public void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDir = (mousePos - aim.position).normalized;

        float turnVelo = 0f;

        float targetangle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(aim.eulerAngles.z, targetangle, ref turnVelo, turnSmoothTime);
                        
        aim.eulerAngles = new Vector3(0, 0, angle);

        SpriteRenderer playerSprite = transform.Find("GFX").GetComponent<SpriteRenderer>();
        SpriteRenderer gunSprite = aim.GetChild(0).GetComponentInChildren<SpriteRenderer>();

        if (aimDir.x < 0)
        {
            gunSprite.flipY = true;
            playerSprite.flipX = true;
        }
        else
        {
            gunSprite.flipY = false;
            playerSprite.flipX = false;
        }
    }

    public void OnShoot()
    {

        if (!_Dashing && Time.time > nextShootTime)
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

    public void OnDash()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.up, .1f, splatterMask);
        if (hit2D)
        {
            if (!_Dashing) positionBeforeDash = transform.position;
            _Dashing = true;
            Debug.Log("Can Dash");

            foreach (SpriteRenderer renderer in renderers)
            {
                Color normal = renderer.color;
                Color transparent = new Color(normal.r, normal.g, normal.b, 0.5f);
                renderer.color = transparent;
            }
        }
        else
        {
            OnExitDash();
        }

    }

    public void OnExitDash()
    {
        _Dashing = false;

        foreach (SpriteRenderer renderer in renderers)
        {
            Color normal = renderer.color;
            Color opegue = new Color(normal.r, normal.g, normal.b, 1f);
            renderer.color = opegue;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        GameManager.instance.OnPlayerHit();
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
