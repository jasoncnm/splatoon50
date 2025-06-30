using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator animator;

    public void PlayIdelAnimation(Vector2 lookDir)
    {
        float absX = Mathf.Abs(lookDir.x);
        float absY = Mathf.Abs(lookDir.y);

        if (absX > absY)
        {
            if (lookDir.x > 0)
            {
                animator.SetBool("Idel_Right", true);

                animator.SetBool("Idel_Left", false);
                animator.SetBool("Idel_Up", false);
                animator.SetBool("Idel_Down", false);
                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", false);
                animator.SetBool("Run_Up", false);
                animator.SetBool("Run_Down", false);
            }
            else
            {
                animator.SetBool("Idel_Right", false);
                animator.SetBool("Idel_Left", true);
                animator.SetBool("Idel_Up", false);
                animator.SetBool("Idel_Down", false);

                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", false);
                animator.SetBool("Run_Up", false);
                animator.SetBool("Run_Down", false);
            }
        }
        else
        {
            if (lookDir.y > 0)
            {
                animator.SetBool("Idel_Right", false);
                animator.SetBool("Idel_Left", false);
                animator.SetBool("Idel_Up", true);
                animator.SetBool("Idel_Down", false);

                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", false);
                animator.SetBool("Run_Up", false);
                animator.SetBool("Run_Down", false);
            }
            else
            {
                animator.SetBool("Idel_Right", false);
                animator.SetBool("Idel_Left", false);
                animator.SetBool("Idel_Up", false);
                animator.SetBool("Idel_Down", true);

                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", false);
                animator.SetBool("Run_Up", false);
                animator.SetBool("Run_Down", false);
            }
        }

    }

    public  void PlayMoveAnimation(Vector2 lookDir)
    {
        float absX = Mathf.Abs(lookDir.x);
        float absY = Mathf.Abs(lookDir.y);

        if (absX > absY)
        {
            if (lookDir.x > 0)
            {
                animator.SetBool("Idel_Right", false);

                animator.SetBool("Idel_Left", false);
                animator.SetBool("Idel_Up", false);
                animator.SetBool("Idel_Down", false);
                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", true);
                animator.SetBool("Run_Up", false);
                animator.SetBool("Run_Down", false);
            }
            else
            {
                animator.SetBool("Idel_Right", false);
                animator.SetBool("Idel_Left", false);
                animator.SetBool("Idel_Up", false);
                animator.SetBool("Idel_Down", false);

                animator.SetBool("Run_Left", true);
                animator.SetBool("Run_Right", false);
                animator.SetBool("Run_Up", false);
                animator.SetBool("Run_Down", false);
            }
        }
        else
        {
            if (lookDir.y > 0)
            {
                animator.SetBool("Idel_Right", false);
                animator.SetBool("Idel_Left", false);
                animator.SetBool("Idel_Up", false);
                animator.SetBool("Idel_Down", false);

                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", false);
                animator.SetBool("Run_Up", true);
                animator.SetBool("Run_Down", false);
            }
            else
            {
                animator.SetBool("Idel_Right", false);
                animator.SetBool("Idel_Left", false);
                animator.SetBool("Idel_Up", false);
                animator.SetBool("Idel_Down", false);

                animator.SetBool("Run_Left", false);
                animator.SetBool("Run_Right", false);
                animator.SetBool("Run_Up", false);
                animator.SetBool("Run_Down", true);
            }
        }
    }


}
