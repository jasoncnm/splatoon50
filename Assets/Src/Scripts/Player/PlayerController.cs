using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f, acceleration = 10f;

    [SerializeField] Transform gunEndPointTr;
    Rigidbody2D rb2D;

    public Transform aim { get; private set; }

    public event EventHandler<OnShootEventArgs> shoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPos;
        public Vector3 shootPos;
    }

    float velPower = 0.7f;
    float turnSmoothTime = 0.01f;

    private void Start()
    {
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
        shoot?.Invoke(this, new OnShootEventArgs
        {
            gunEndPointPos = gunEndPointTr.position,
            shootPos = Util.GetMouseWorldPosition()
        });
    }


}
