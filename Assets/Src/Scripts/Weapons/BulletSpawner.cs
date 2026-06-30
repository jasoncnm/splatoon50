using UnityEngine;
using UnityEngine.UIElements;

public class BulletSpawner : MonoBehaviour
{
    public BulletsSO bullets;

    [SerializeField] Transform muzzleFlash;

    public void SpawnBulllet(Vector3 pos, Vector3 dir)
    {
        GunProperties gp = GetComponent<GunProperties>();

        float damage = gp.damage;
        float fallOffDistance = gp.fallOffDistance;
        float pierce = gp.pierce;
        float spread = gp.spread;

        float angle = Util.GetAngleFromDirectionalVector(dir);

        float offset = Random.Range(-spread, spread);

        angle += offset;

        Quaternion rot = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

        Transform bullet = bullets.normalBullet;

        switch (gp.elementalDamage)
        {
            case ElementalDamage.FIRE:
                {
                    bullet = bullets.fireBullet;
                    break;
                }
            case ElementalDamage.ICE:
                {
                    bullet = bullets.iceBullet;
                    break;
                }
            case ElementalDamage.LIGHTING:
                {
                    bullet = bullets.lightingBullet;
                    break;
                }
            case ElementalDamage.POSION:
                {

                    break;
                }
            case ElementalDamage.NONE:
                {
                    bullet = bullets.normalBullet;
                    break;
                }
        }

        // Test

        Transform tr = Instantiate(bullet, pos, rot);

        dir = tr.right;

        float rand = Random.Range(0f, 1f);

        if (rand < gp.crits)
        {
            Debug.Log("Critical Hit!");
            damage *= 2;
        }

        Debug.Log(damage);

        tr.GetComponent<Bullet>().Setup(dir, damage, pierce, fallOffDistance, gp.elementalDamage);

        if (muzzleFlash) muzzleFlash.GetComponent<MuzzleFlash>().Setup();

    }

}
