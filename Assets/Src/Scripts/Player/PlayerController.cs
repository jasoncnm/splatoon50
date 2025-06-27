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

    new SpriteRenderer renderer;

    public Transform aim { get; private set; }

    public event EventHandler<OnShootEventArgs> shoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPos;
        public Vector3 shootPos;
        public Vector3 shootDir;
    }

    float velPower = 0.7f;
    float turnSmoothTime = 0.01f;
    float nextShootTime = 0f;


    public bool _Dashing { get; private set; } = false;

    private void Start()
    {
        renderer = transform.GetComponent<SpriteRenderer>();
        aim = transform;
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Vector2 targetVelocity = direction * moveSpeed;
        Vector2 VelocityDiff = targetVelocity - rb2D.linearVelocity;

        float accelRate = acceleration;

        Vector2 force = new Vector3(
            Mathf.Pow(Mathf.Abs(VelocityDiff.x) * accelRate, velPower) * Mathf.Sign(VelocityDiff.x),
            Mathf.Pow(Mathf.Abs(VelocityDiff.y) * accelRate, velPower) * Mathf.Sign(VelocityDiff.y)
            );

        rb2D.AddForce(force);
    }

    public void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDir = (mousePos - transform.position).normalized;

        float turnVelo = 0f;

        float targetangle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetangle, ref turnVelo, turnSmoothTime);


        aim.eulerAngles = new Vector3(0, 0, angle);

    }

    public void OnShoot()
    {

        if (!_Dashing && Time.time > nextShootTime)
        {

            Vector3 endPointPos = gunEndPointTr.position;
            Vector3 shootPos = Util.GetMouseWorldPosition();
            Vector3 shootDir = shootPos - endPointPos;

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
        Color normal = renderer.color;
        Color transparent = new Color(normal.r, normal.g, normal.b, 0.5f);
       
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.up, .1f, splatterMask);
        if (hit2D)
        {
            _Dashing = true;
            Debug.Log("Can Dash");
            renderer.color = transparent;
        }
        else
        {
            OnExitDash();
        }

    }

    public void OnExitDash()
    {
        _Dashing = false;
        Color normal = renderer.color;
        Color opegue = new Color(normal.r, normal.g, normal.b, 1f);
        renderer.color = opegue;

    }

    private void OnParticleCollision(GameObject other)
    {
        GameManager.instance.OnPlayerHit();
    }

}
