using UnityEngine;

public class test_animation : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Play explosion
            animator.SetTrigger("Trigger");
        }
    }

}
