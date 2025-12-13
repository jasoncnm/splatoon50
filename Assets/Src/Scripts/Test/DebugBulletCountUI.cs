using TMPro;
using UnityEngine;

public class BulletCountUI : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnBulletShot(PlayerGunController gunController)
    {
        int count = gunController.bulletLeft;

        text.text = "Bullet: " + count.ToString() + " / " + gunController.maxBullet.ToString();

    }

}
