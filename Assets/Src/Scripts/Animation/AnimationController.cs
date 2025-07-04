using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator animator;

    public void PlayIdelAnimation()
    {
        animator.SetBool("Move", false);
    }

    public  void PlayMoveAnimation(Vector2 lookDir)
    {
        animator.SetBool("Move",true);
    }


}
