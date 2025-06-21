using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;

    Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.linearVelocityX = moveSpeed;
    }
}
