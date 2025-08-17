using UnityEngine;
using UnityEngine.UI;

public class ReloadSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Animator animator;

    void Start()
    {
        slider.gameObject.SetActive(false);
    }

    public void OnReloadStart(PlayerGunController gunController)
    {
        animator.speed = 1 / gunController.reloadTime;
        slider.gameObject.SetActive(true);
    }

    public void OnReloadEnd()
    {

        slider.gameObject.SetActive(false);
    }

}
